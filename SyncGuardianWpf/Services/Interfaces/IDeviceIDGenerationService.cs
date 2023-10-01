using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncGuardianWpf.Services.Interfaces
{
    public interface IDeviceIDGenerationService
    {
        string GenerateDeviceId();
        bool ValidateDeviceID(string hash);
        bool NeedInitialSetup();
        bool ResetDeviceId();
        string ComputeHash(string inputString);
        string GetCPUDetails();
        string GetGPUDetails();
        string GetWindowsVersion();
        string GenerateUniqueGUID();
        bool SaveGUIDToRegistry(string guid);
        string ReadGUIDFromRegistry();
        string GenerateConcatedInfo(string cpu, string gpu, string windowsVersion, string guid);
    }
}
