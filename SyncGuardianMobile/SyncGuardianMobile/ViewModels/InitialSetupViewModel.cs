using Microsoft.Extensions.DependencyInjection;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SyncGuardianMobile.Commands;
using SyncGuardianMobile.Helpers;
using SyncGuardianMobile.Models;
using SyncGuardianMobile.Services.Interface;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SyncGuardianMobile.ViewModels
{
    public class InitialSetupViewModel : BaseViewModel
    {
        private IServiceProvider _serviceProvider;
        public InitialSetupViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            StartQRScanningCommand = new RelayCommands(ExecuteStartQRScanning, CanStartQRScanning);
            MoveToMainMenuCommand = new RelayCommands(ExecuteMoveToMainMenu);

            CurrentPage = InitialSetup_NavStore.INTRODUCTION;
        }

        #region Attributes

#nullable enable
        private Task<PermissionStatus>? CameraPermissionStatus = null;

        private string _qrResults;

        public string QRResults
        {
            get { return _qrResults; }
            set
            {
                if (_qrResults != value)
                {
                    _qrResults = value;
                    OnPropertyChanged(nameof(QRResults));
                }
            }
        }

        private string _currentPage;

        public string CurrentPage
        {
            get { return _currentPage; }
            private set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged(nameof(CurrentPage));
                }
            }
        }

        #endregion

        #region Commands

        public ICommand MoveToMainMenuCommand { get; private set; }
        private void ExecuteMoveToMainMenu(object obj)
        {
            App.Current.MainPage = _serviceProvider.GetRequiredService<AppShell>();
        }

        public ICommand StartQRScanningCommand { get; private set; }
        private bool CanStartQRScanning(object arg)
        {
            return true;
        }
        private async void ExecuteStartQRScanning(object obj)
        {
            try
            {
                var permissionStatus = await IsCameraAllowed();
                if (!permissionStatus)
                {
                    await RequestCameraPermission();
                    permissionStatus = await IsCameraAllowed();
                }

                if (permissionStatus)
                {
                    var scanner = _serviceProvider.GetRequiredService<IQrScanning>();
                    var result = await scanner.ScanAsync();
                    if (result != null && result.ToClass<SetupQRCodeModel>() != null)
                    {
                        this.QRResults = result;
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private async Task<bool> IsCameraAllowed()
        {
            var permission = await CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>();
            return permission.Equals(PermissionStatus.Granted);
        }

        private async Task<bool> RequestCameraPermission()
        {
            try
            {
                await App.Current.MainPage.DisplayAlert("Camera Permission", "We need access to your camera to scan QR codes. Please allow us to use your camera.", "OK");
                if (CameraPermissionStatus != null)
                {
                    CrossPermissions.Current.OpenAppSettings();
                    return false;
                }

                var result = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();

                return result.Equals(PermissionStatus.Granted);
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
