﻿namespace SG.Common.Models
{
    public class SetupQrCodeModel
    {
        public string DeviceHash { get; set; } = default!;
        public string NetworkAddress { get; set; } = default!;
        public int Port { get; set; }
        public string Protocal { get; set; } = default!;
    }
}
