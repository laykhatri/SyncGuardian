using SyncGuardianWpf.Services.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Management;

namespace SyncGuardianWpf.Services
{
    public class DeviceIDGenerationService : IDeviceIDGenerationService
    {
        public string GenerateDeviceId()
        {
            var windowsVersion = GetWindowsVersion();
            var cpuDetails = GetCPUDetails();
            var gpuDetails = GetGPUDetails();
            var guidId = Guid.NewGuid();

            string concatenatedInfo = $"{cpuDetails}-{gpuDetails}-{windowsVersion}-{guidId}";

            return ComputeHash(concatenatedInfo);
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
    }
}
