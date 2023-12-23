using System.Collections.Generic;

namespace SyncGuardianMobile.Helpers
{
    public static class SharedPrefsConstants
    {
        public const string DEFAULT_PREFS = "SyncGuardianPrefs";
        public const string DEVICEID_PREFS = "SGP_DeviceID";
    }

    public static class InitialSetup_NavStore
    {
        public const string INTRODUCTION = "Introduction";

        public static HashSet<string> InitialSetup_Pages = new HashSet<string>()
        {
            INTRODUCTION
        };
    }
}
