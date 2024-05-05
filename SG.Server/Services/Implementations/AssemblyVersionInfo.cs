using SG.Server.Services.Interfaces;
using System;
using System.Reflection;

namespace SG.Server.Services.Implementations
{
    internal class AssemblyVersionInfo : IAssemblyVersionInfo
    {
        public string GetVersionInfo()
        {
            return Convert.ToString(Assembly.GetExecutingAssembly().GetName().Version) ?? string.Empty;
        }
    }
}
