using Serilog;
using SG.Server.Services.Interfaces;

namespace SG.Server.Services.Implementations
{
    internal class DeviceIDGenerationService : IDeviceIDGenerationService
    {
        private readonly ILogger _logger;

        public DeviceIDGenerationService(ILogger logger)
        {
            _logger = logger;
        }

    }
}
