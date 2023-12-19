using SyncGuardianMobile.Services.Interface;
using Xamarin.Essentials;
using System;
using SyncGuardianMobile.Helpers;

namespace SyncGuardianMobile.Services
{
    public class SharedPrefs : ISharedPrefs
    {
        private string _prefKey;

        public SharedPrefs()
        {
            _prefKey = SharedPrefsConstants.DEFAULT_PREFS;
        }

        public SharedPrefs(string prefKey)
        {
            _prefKey= prefKey;
        }

        public T GetValue<T>(string key)
        {
            object value = Preferences.Get(key, null, _prefKey);
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public void SetValue<T>(string key, T value)
        {
            string stringValue = Convert.ToString(value);
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.String:
                    Preferences.Set(key, stringValue, _prefKey);
                    break;
                case TypeCode.Boolean:
                    Preferences.Set(key, bool.Parse(stringValue), _prefKey);
                    break;
                case TypeCode.Int32:
                    Preferences.Set(key, int.Parse(stringValue), _prefKey);
                    break;
                case TypeCode.Int64:
                    Preferences.Set(key, long.Parse(stringValue), _prefKey);
                    break;
                case TypeCode.Double:
                    Preferences.Set(key, double.Parse(stringValue), _prefKey);
                    break;
                case TypeCode.Decimal:
                    Preferences.Set(key, float.Parse(stringValue), _prefKey);
                    break;
                case TypeCode.DateTime:
                    Preferences.Set(key, DateTime.Parse(stringValue), _prefKey);
                    break;
                default:
                    throw new ArgumentException("Unsupported type");
            }
        }
    }
}
