using SyncGuardianWpf.Services.Interfaces;
using System.Net.NetworkInformation;

namespace SyncGuardianWpf.Services
{
    public class NetworkInterfaceService : INetworkInterfaceService
    {
        public string GetLocalIpAddress()
        {
            string ipAddress = string.Empty;
            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.OperationalStatus == OperationalStatus.Up && (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    )
                {
                    foreach (UnicastIPAddressInformation ip in netInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            ipAddress = ip.Address.ToString();
                            break;
                        }
                    }
                }
            }
            return ipAddress;
        }

        public bool IsNetworkConnected()
        {
            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Check if the network interface is up and connected
                if (netInterface.OperationalStatus == OperationalStatus.Up &&
                    (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
