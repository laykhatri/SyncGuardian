using Microsoft.Win32;
using Serilog;
using SG.Server.Helpers;
using SG.Server.Services.Interfaces;
using System;
using System.Management;
using System.Text;

namespace SG.Server.Services.Implementations
{
    internal class DeviceIDGenerationService : IDeviceIDGenerationService
    {
        private readonly ILogger _logger;

        public DeviceIDGenerationService(ILogger logger)
        {
            _logger = logger;
        }

        #region Device Id Methods
        public string GenerateDeviceId()
        {
            var guidId = GenerateUniqueGUID();

            if (SaveGUIDToRegistry(guidId))
            {
                string concatenatedInfo = GenerateConcatedInfo(GetCPUDetails(), GetGPUDetails(), GetOSVersion(), guidId);
                return concatenatedInfo.ConvertToHash();
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
                var concatenatedInfo = GenerateConcatedInfo(GetCPUDetails(), GetGPUDetails(), GetOSVersion(), deviceGuid);
                return concatenatedInfo.ConvertToHash().Equals(hash);

            }
            return false;
        }
        #endregion

        #region Hardware information Methods
        public string GetCPUDetails()
        {
            StringBuilder builder = new StringBuilder();

#pragma warning disable CA1416 // Validate platform compatibility. Only Supports Windows as of now
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT name,ProcessorId FROM Win32_processor");

            foreach (ManagementObject mo in searcher.Get())
            {
                builder.Append(mo["ProcessorId"].ToString());
                builder.Append("=");
                builder.Append(mo["Name"].ToString());
                break;
            }
#pragma warning restore CA1416 // Validate platform compatibility

            return builder.ToString();
        }

        public string GetGPUDetails()
        {
            StringBuilder builder = new StringBuilder();

#pragma warning disable CA1416 // Validate platform compatibility. Only Supports Windows as of now
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Name FROM Win32_videocontroller");

            foreach (ManagementObject mo in searcher.Get())
            {
                builder.Append(mo["Name"].ToString());
                break;
            }
#pragma warning restore CA1416 // Validate platform compatibility

            return builder.ToString();
        }
        #endregion

        #region OS Version method
        public string GetOSVersion()
        {
            return Environment.OSVersion.Version.Major.ToString();
        }
        #endregion

        #region Registry Methods
        public string ReadGUIDFromRegistry()
        {
            try
            {
#pragma warning disable CA1416 // Validate platform compatibility. Only Supports Windows as of now
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\SyncGuardian"))
                {
                    if (key != null)
                    {
                        return Convert.ToString(key.GetValue("UniqueGUID") as string)!;
                    }
                }
#pragma warning restore CA1416 // Validate platform compatibility

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error("Error in Reading GUID From Registry", ex);
                return null!;
            }
        }

        public bool SaveGUIDToRegistry(string guid)
        {
            try
            {
#pragma warning disable CA1416 // Validate platform compatibility. Only Supports Windows as of now
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\SyncGuardian"))
                {
                    key.SetValue("UniqueGUID", guid);
                }
#pragma warning restore CA1416 // Validate platform compatibility

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("Error in Saving GUID To Registry", ex);
                return false;
            }
        }
        #endregion

        #region Helper Methods
        public string GenerateUniqueGUID()
        {
            return Guid.NewGuid().ToString();
        }

        public string GenerateConcatedInfo(string cpu, string gpu, string windowsVersion, string guid)
        {
            return $"{cpu}-{gpu}-{windowsVersion}-{guid}";
        }

        public bool NeedInitialSetup()
        {
            return ReadGUIDFromRegistry().IsNullOrWhiteSpace();
        }
        #endregion
    }
}
