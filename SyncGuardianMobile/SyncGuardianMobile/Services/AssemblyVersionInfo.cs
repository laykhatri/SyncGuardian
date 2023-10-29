using SyncGuardianMobile.Services.Interface;
using System;
using System.Reflection;

namespace SyncGuardianMobile.Services
{
    public class AssemblyVersionInfo : IAssemblyVersionInfo
    {
        public string GetVersionInfo()
        {
            return Convert.ToString(Assembly.GetExecutingAssembly().GetName().Version);
        }
    }
}
