using Android.OS;
using Android.Util;
using SyncGuardianMobile.Helpers;
using SyncGuardianMobile.Services.Interface;
using System;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Essentials;

namespace SyncGuardianMobile.Services
{
    public class DeviceIDGenerator : IDeviceIDGenerator
    {
        private readonly ISharedPrefs _sharedPrefs;

        private const string GUIDKEY = "DeviceId";

        public DeviceIDGenerator()
        {
            _sharedPrefs = new SharedPrefs(SharedPrefsConstants.DEVICEID_PREFS);
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

        public string GenerateConcatedInfo(string cpu, string fingerprint, string odmSku, string socManufacturer, string guid)
        {
            return $"{cpu}-{fingerprint}-{odmSku}-{socManufacturer}-{guid}";
        }

        public string GenerateUniqueGUID()
        {
            return Guid.NewGuid().ToString();
        }

        [Obsolete]
        public string GetCPUDetails()
        {
            return Convert.ToString(Build.CpuAbi);
        }

        public string GetFingerPrint()
        {
            return Convert.ToString(Build.Fingerprint);
        }

        public string GetODMSKU()
        {
            return Convert.ToString(Build.OdmSku);
        }

        public string GetSocManufacturer()
        {
            return Convert.ToString(Build.SocManufacturer);
        }

        public string ReadGuidFromPrefs()
        {
            try
            {
                return _sharedPrefs.GetValue<string>(GUIDKEY);
            }
            catch(Exception ex) {
                Log.Error(nameof(DeviceIDGenerator), ex.Message, ex);
                throw;
            }
        }

        public bool SaveGuidToPrefs(string guid)
        {
            try
            {
                _sharedPrefs.SetValue<string>(GUIDKEY, guid);

                return true;
            }
            catch(Exception ex)
            {
                Log.Error(nameof(DeviceIDGenerator),ex.Message, ex);
                return false;
            }
        }

        public bool NeedInitialSetup()
        {
            return string.IsNullOrWhiteSpace(ReadGuidFromPrefs());
        }

        [Obsolete]
        public string GenerateDeviceId()
        {
            var guid = GenerateUniqueGUID();

            if(SaveGuidToPrefs(guid))
            {
                var concatedInfo = GenerateConcatedInfo(GetCPUDetails(),GetFingerPrint(),GetODMSKU(),GetSocManufacturer(), guid);
                return ComputeHash(concatedInfo);
            }
            else
            {
                return null;
            }
        }

        [Obsolete]
        public bool ValidateDeviceId(string hash)
        {
            if (!string.IsNullOrWhiteSpace(ReadGuidFromPrefs()))
            {
                var deviceGuid = ReadGuidFromPrefs();
                var concatedInfo = GenerateConcatedInfo(GetCPUDetails(), GetFingerPrint(), GetODMSKU(), GetSocManufacturer(), deviceGuid);
                return ComputeHash(concatedInfo).Equals(hash);
            }
            return false;
        }

        public bool ResetDeviceId()
        {
            try
            {
                Preferences.Remove(GUIDKEY);
                return true;
            }
            catch(Exception ex)
            {
                Log.Error(nameof(DeviceIDGenerator), ex.Message, ex);
                return false;
            }
        }
    }
}
