namespace SG.Server.Services.Interfaces
{
    public interface INetworkInterfaceService
    {
        bool IsNetworkConnected();
        string GetLocalIpAddress();
    }
}
