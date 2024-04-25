using ReactiveUI;
using Serilog;
using SG.Server.Helpers;
using SG.Server.Services.Interfaces;

namespace SG.Server.ViewModels
{
    public class InitialSetupViewModel : ViewModelBase
    {
        private readonly ILogger _logger;
        private readonly INetworkInterfaceService _networkService;
        private readonly IDeviceIDGenerationService _deviceIDGenerationService;
        private readonly IQRCodeGenerationService _qrCodeGenerationService;
        public InitialSetupViewModel(ILogger logger, INetworkInterfaceService networkService, IDeviceIDGenerationService deviceIDGenerationService, IQRCodeGenerationService qrCodeGenerationService)
        {
            _logger = logger;
            _networkService = networkService;
            _deviceIDGenerationService = deviceIDGenerationService;
            _qrCodeGenerationService = qrCodeGenerationService;
            InitialSetup();
        }

        #region Methods
        private void InitialSetup()
        {
            _logger.LogSgInfo("Checking if device is connected to any network");

            if (!_networkService.IsNetworkConnected())
            {
                IsSetupGridVisible = false;
            }
            else
            {
                IsSetupGridVisible = true;
                _logger.Information("Generating ");
                var deviceId = _deviceIDGenerationService.GenerateDeviceId();
                if (!deviceId.IsNullOrWhiteSpace())
                {

                }
            }
        }
        #endregion

        #region Properties
        private bool _isSetupGridVisible;

        public bool IsSetupGridVisible
        {
            get { return _isSetupGridVisible; }
            set { this.RaiseAndSetIfChanged(ref _isSetupGridVisible, value); }
        }

        #endregion
    }
}
