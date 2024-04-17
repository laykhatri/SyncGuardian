using SG.Server.Services.Interfaces;
using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace SG.Server.Services.Implementations
{
    internal class NetworkInterfaceService : INetworkInterfaceService
    {
        public string GetLocalIpAddress()
        {
            var gatewayAddress = NetworkInterface.GetAllNetworkInterfaces()
                                        .Where(ni => ni.OperationalStatus == OperationalStatus.Up)
                                        .SelectMany(ni => ni.GetIPProperties().GatewayAddresses)
                                        .FirstOrDefault(ga => ga != null);

            return gatewayAddress?.Address.ToString() ?? throw new InvalidOperationException("No gateway address found.");
        }

        public bool IsNetworkConnected()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
    }
}
