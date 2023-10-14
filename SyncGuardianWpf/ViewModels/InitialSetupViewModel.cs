using Serilog;
using SyncGuardianWpf.Models;
using SyncGuardianWpf.Services.Interfaces;
using System.Text.Json;
using System.Windows.Media.Imaging;

namespace SyncGuardianWpf.ViewModels
{
    public class InitialSetupViewModel : ViewBaseModel
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

        public void InitialSetup()
        {
            _logger.Information("Checking is device connected to any network");
            if (!_networkService.IsNetworkConnected())
            {
                SetupGridVisible = false;
            }
            else
            {
                SetupGridVisible = true;
                var DeviceId = _deviceIDGenerationService.GenerateDeviceId();
                if (DeviceId != null)
                {
                    var QRCodeInfo = new SetupQRCodeModel() { DeviceHash = DeviceId, Port = 54214, Protocal = "HTTPS", NetworkAddress = _networkService.GetLocalIpAddress() };
                    QRCodeImage = _qrCodeGenerationService.GenerateQRCodeAndConvertToBitmapImage(JsonSerializer.Serialize(QRCodeInfo));
                }
            }
        }

        private bool _isSetupGridVisible;

        public bool SetupGridVisible
        {
            get { return _isSetupGridVisible; }
            set { _isSetupGridVisible = value; OnPropertyChanged(nameof(SetupGridVisible)); }
        }

        private BitmapImage _qrCodeImage = default!;

        public BitmapImage QRCodeImage
        {
            get { return _qrCodeImage; }
            set { _qrCodeImage = value; OnPropertyChanged(nameof(QRCodeImage)); }
        }

    }
}
