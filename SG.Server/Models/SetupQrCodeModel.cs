namespace SG.Server.Models
{
    internal class SetupQrCodeModel
    {
        public string DeviceHash { get; set; } = default!;
        public string NetworkAddress { get; set; } = default!;
        public int Port { get; set; }
        public string Protocal { get; set; } = default!;
    }
}
