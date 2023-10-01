using SyncGuardianWpf.Services.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Management;
using Microsoft.Win32;
using Serilog;
using SyncGuardianWpf.Helpers;

namespace SyncGuardianWpf.Services
{
    public class DeviceIDGenerationService : IDeviceIDGenerationService
    {
        private readonly ILogger _logger;

        public DeviceIDGenerationService(ILogger logger)
        {
            _logger = logger;
        }

        public string GenerateDeviceId()
        {
            var guidId = GenerateUniqueGUID();

            if (SaveGUIDToRegistry(guidId))
            {
                string concatenatedInfo = GenerateConcatedInfo(GetCPUDetails(), GetGPUDetails(), GetWindowsVersion(), guidId);
                return ComputeHash(concatenatedInfo);
            }
            else
            {
                return null!;
            }
        }

        public bool ValidateDeviceID(string hash)
        {
            if (!string.IsNullOrWhiteSpace(ReadGUIDFromRegistry()))
            {
                var deviceGuid = ReadGUIDFromRegistry();
                var concataenatedInfo = GenerateConcatedInfo(GetCPUDetails(), GetGPUDetails(), GetWindowsVersion(), deviceGuid);
                return ComputeHash(concataenatedInfo).Equals(hash);

            }
            return false;
        }

        public bool NeedInitialSetup()
        {
            return string.IsNullOrWhiteSpace(ReadGUIDFromRegistry());
        }

        public bool ResetDeviceId()
        {
            try
            {
                RegistryKey baseRegistryKey = Registry.CurrentUser;
                RegistryKey registryKey = baseRegistryKey.OpenSubKey(RegistryName.KEYPATH, true)!;

                if (registryKey != null)
                {
                    registryKey.DeleteSubKeyTree(RegistryName.KEYNAME);
                    registryKey.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error removing DeviceID", ex);
            }
            return false;
        }

        public string ComputeHash(string inputString)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                // Convert the byte array to a hexadecimal string

                // Using StringBuilder for faster string concat
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2")); // Use "x2" to format as hexadecimal
                }

                return builder.ToString();
            }
        }

        public string GetCPUDetails()
        {
            StringBuilder builder = new StringBuilder();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT name,ProcessorId FROM Win32_processor");

            foreach (ManagementObject mo in searcher.Get())
            {
                builder.Append(mo["ProcessorId"].ToString());
                builder.Append("=");
                builder.Append(mo["Name"].ToString());
                break;
            }

            return builder.ToString();
        }

        public string GetGPUDetails()
        {
            StringBuilder builder = new StringBuilder();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Name FROM Win32_videocontroller");

            foreach (ManagementObject mo in searcher.Get())
            {
                builder.Append(mo["Name"].ToString());
                break;
            }

            return builder.ToString();
        }

        public string GetWindowsVersion()
        {
            return Environment.OSVersion.Version.Major.ToString();
        }

        public string GenerateUniqueGUID()
        {
            return Guid.NewGuid().ToString();
        }

        public bool SaveGUIDToRegistry(string guid)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\SyncGuardian"))
                {
                    key.SetValue("UniqueGUID", guid);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("Error in Saving GUID To Registry", ex);
                return false;
            }
        }

        public string ReadGUIDFromRegistry()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\SyncGuardian"))
                {
                    if (key != null)
                    {
                        return Convert.ToString(key.GetValue("UniqueGUID") as string)!;
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error("Error in Reading GUID From Registry", ex);
                return null!;
            }
        }

        public string GenerateConcatedInfo(string cpu, string gpu, string windowsVersion, string guid)
        {
            return $"{cpu}-{gpu}-{windowsVersion}-{guid}";
        }

    }
}
