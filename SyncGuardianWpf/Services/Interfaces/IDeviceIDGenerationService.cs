using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncGuardianWpf.Services.Interfaces
{
     public interface IDeviceIDGenerationService
    {
        string GetCPUDetails();
        string GetGPUDetails();
        string GetWindowsVersion();
        string ComputeHash(string inputString);
        string GenerateDeviceId();

    }
}
