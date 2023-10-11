using SyncGuardianWpf.Services.Interfaces;
using System;
using System.Reflection;

namespace SyncGuardianWpf.Services
{
    public class AssemblyVersionInfo : IAssemblyVersionInfo
    {
        public string GetVersionInfo()
        {
            return Convert.ToString(Assembly.GetExecutingAssembly().GetName().Version)!;
        }
    }
}
