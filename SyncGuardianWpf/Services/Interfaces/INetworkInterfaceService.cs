using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncGuardianWpf.Services.Interfaces
{
    public interface INetworkInterfaceService
    {
        bool IsNetworkConnected();
        string GetLocalIpAddress();
    }
}
