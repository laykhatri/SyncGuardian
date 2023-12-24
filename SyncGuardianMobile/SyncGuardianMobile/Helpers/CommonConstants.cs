using System.Collections.Generic;

namespace SyncGuardianMobile.Helpers
{
    public static class UrlConstants
    {
        public const string GIT_REPO_RELEASES = "https://github.com/laykhatri/SyncGuardian/releases";
        public const string GIT_REPO = "https://github.com/laykhatri/SyncGuardian";
    }
    public static class SharedPrefsConstants
    {
        public const string DEFAULT_PREFS = "SyncGuardianPrefs";
        public const string DEVICEID_PREFS = "SGP_DeviceID";
    }

    public static class InitialSetup_NavStore
    {
        public const string INTRODUCTION = "Introduction";
        public const string PERMISSION = "Permission";
        public const string CONNECTION = "Connection";
        public const string SUMMARY = "Summary";


        public static IReadOnlyCollection<string> InitialSetup_Pages = new List<string>()
        {
            INTRODUCTION,
            PERMISSION,
            CONNECTION,
            SUMMARY
        };
    }
}
