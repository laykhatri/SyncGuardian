using Avalonia.Media.Imaging;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Serilog;
using SG.Common.Models;
using SG.Server.Helpers;
using SG.Server.Services.Interfaces;
using System.Text.Json;

namespace SG.Server.ViewModels
{
    public class InitialSetupViewModel : ViewModelBase
    {
        private readonly ILogger _logger;
        private readonly INetworkInterfaceService _networkService;
        private readonly IDeviceIDGenerationService _deviceIDGenerationService;
        private readonly IQRCodeGenerationService _qrCodeGenerationService;

        public InitialSetupViewModel()
        {
            _logger = App.ServiceProvider.GetRequiredService<ILogger>();
            _networkService = App.ServiceProvider.GetRequiredService<INetworkInterfaceService>();
            _deviceIDGenerationService = App.ServiceProvider.GetRequiredService<IDeviceIDGenerationService>();
            _qrCodeGenerationService = App.ServiceProvider.GetRequiredService<IQRCodeGenerationService>();
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
                _logger.Information("Generating QrCode to scan");
                var deviceId = _deviceIDGenerationService.GenerateDeviceId();
                if (!deviceId.IsNullOrWhiteSpace())
                {
                    var QRCodeInfo = new SetupQrCodeModel() { DeviceHash = deviceId, Port = 54214, Protocal = "HTTPS", NetworkAddress = _networkService.GetLocalIpAddress() };
                    QRCodeImage = _qrCodeGenerationService.GenerateQRCodeAndConvertToBitmapImage(JsonSerializer.Serialize(QRCodeInfo));
                }
                IsSetupGridVisible = true;
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

        private Bitmap? _qrCode;

        public Bitmap? QRCodeImage
        {
            get { return _qrCode; }
            set { this.RaiseAndSetIfChanged(ref _qrCode, value); }
        }


        #endregion
    }
}
