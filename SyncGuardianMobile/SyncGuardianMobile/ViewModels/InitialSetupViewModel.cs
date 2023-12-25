using Microsoft.Extensions.DependencyInjection;
using Plugin.Permissions;
using SyncGuardianMobile.Commands;
using SyncGuardianMobile.Helpers;
using SyncGuardianMobile.Models;
using SyncGuardianMobile.Services.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace SyncGuardianMobile.ViewModels
{
    public class InitialSetupViewModel : BaseViewModel
    {
        private IServiceProvider _serviceProvider;
        private IHttpService _httpService;
        public InitialSetupViewModel(IServiceProvider serviceProvider, IHttpService httpService)
        {
            _serviceProvider = serviceProvider;
            _httpService = httpService;

            MoveToMainMenuCommand = new RelayCommands(ExecuteMoveToMainMenu);
            NextButtonCommand = new RelayCommands(ExecuteNextButton);
            BackButtonCommand = new RelayCommands(ExecuteBackButton);
            OpenSyncGuardianUrl = new RelayCommands(ExecuteOpenSyncGuardianUrl);

            CurrentPage = InitialSetup_NavStore.INTRODUCTION;
            CurrentPageIndex = 0;
        }

        #region Attributes

#nullable enable
        private Task<Plugin.Permissions.Abstractions.PermissionStatus>? CameraPermissionStatus = null;
        private int CurrentPageIndex = -1;

        private SetupQRCodeModel? _qrResults;

        public SetupQRCodeModel? QRResults
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

        private string _connectionStatus;

        public string ConnectionStatus
        {
            get { return _connectionStatus; }
            set
            {
                if (_connectionStatus != value)
                {
                    _connectionStatus = value;
                    OnPropertyChanged(nameof(ConnectionStatus));
                }
            }
        }

        private bool _readyToMainPage;

        public bool ReadyToMainPage
        {
            get { return _readyToMainPage; }
            set
            {
                if (_readyToMainPage != value)
                {
                    _readyToMainPage = value;
                    OnPropertyChanged(nameof(ReadyToMainPage));
                }
            }
        }


        #endregion

        #region Commands

        public ICommand OpenSyncGuardianUrl { get; private set; }

        private void ExecuteOpenSyncGuardianUrl(object obj)
        {
            Launcher.OpenAsync(new Uri(UrlConstants.GIT_REPO_RELEASES));
        }

        public ICommand BackButtonCommand { get; private set; }
        private void ExecuteBackButton(object obj)
        {
            UpdateNavigation(CurrentPageIndex - 1);
        }

        public ICommand NextButtonCommand { get; private set; }
        private void ExecuteNextButton(object obj)
        {
            UpdatePagesAsync().ContinueWith((t) =>
            {
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    UpdateNavigation(CurrentPageIndex + 1);
                }
            });
        }

        public ICommand MoveToMainMenuCommand { get; private set; }
        private void ExecuteMoveToMainMenu(object obj)
        {
            App.Current.MainPage = _serviceProvider.GetRequiredService<AppShell>();
        }

        #endregion

        #region HelperMethods

        private async Task<bool> AuthenticateDevice()
        {
            try
            {
                // TODO: Need to update this function for actual authentication call
                var result = await _httpService.GetAsync(UrlConstants.GIT_REPO);

                if (result.IsSuccessStatusCode)
                {
                    ConnectionStatus += "Authentication Successful\r\n";
                    ReadyToMainPage = true;
                    return true;
                }
                else
                {
                    ConnectionStatus += "Authentication Failed.\r\n";
                    ConnectionStatus += $"{await result.Content.ReadAsStringAsync()}\r\n";
                }
                return false;
            }
            catch (Exception) { throw; }
        }

        private async Task<bool> PopulateQRData()
                {
            int failedStatus = 0;
            do
            {
                try
                {
                    if (await IsCameraAllowed())
                    {
                    var scanner = _serviceProvider.GetRequiredService<IQrScanning>();
                    var result = await scanner.ScanAsync();
                        if (result == null)
                    {
                            this.QRResults = null;
                    }
                        else if (result != null && result.ToClass<SetupQRCodeModel>() != null)
                        {
                            this.QRResults = result.ToClass<SetupQRCodeModel>()!;
                }
            }
                }
            catch (Exception) { throw; }
                if (QRResults == null)
                {
                    failedStatus++;
        }
            } while (QRResults == null && failedStatus < 3);

            if (QRResults == null)
            {
                ConnectionStatus += "Invalid QR Scanned\r\n";
                return false;
            }
            else
            {
                ConnectionStatus += "Valid QR Found\r\n";
                return true;
            }
        }

        private async Task<bool> IsCameraAllowed()
        {
            var permission = await CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>();
            return permission.Equals(Plugin.Permissions.Abstractions.PermissionStatus.Granted);
        }

        private async Task<bool> RequestCameraPermission()
        {
            try
            {
                if (this.CameraPermissionStatus != null)
                {
                    CrossPermissions.Current.OpenAppSettings();
                    return false;
                }

                CameraPermissionStatus = CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();
                var result = await CameraPermissionStatus;
                CameraPermissionStatus = null;
                return result.Equals(Plugin.Permissions.Abstractions.PermissionStatus.Granted);

            }
            catch (Exception) { throw; }
        }

        private void UpdateNavigation(int pageNumber)
        {
            if (pageNumber < 0 || pageNumber > InitialSetup_NavStore.InitialSetup_Pages.Count)
            {
                return;
            }

            CurrentPageIndex = pageNumber;
            CurrentPage = InitialSetup_NavStore.InitialSetup_Pages.ElementAt(CurrentPageIndex);
        }

        private async Task<bool> UpdatePagesAsync()
        {
            switch (CurrentPage)
            {
                case InitialSetup_NavStore.INTRODUCTION:
                    return true;
                case InitialSetup_NavStore.PERMISSION:
                    var permissionStatus = await IsCameraAllowed();
                    if (!permissionStatus)
                    {
                        permissionStatus = await RequestCameraPermission();
                    }
                    return permissionStatus;
                case InitialSetup_NavStore.CONNECTION:
                    if (!await PopulateQRData())
                    {
                        return false;
                    }

                    if (!await AuthenticateDevice())
                    {
                        return false;
                    }

                    return true;
                case InitialSetup_NavStore.SUMMARY:
                    if (ReadyToMainPage)
                    {
                        MoveToMainMenuCommand.Execute(this);
                    }
                    return true;
            }
            return false;
        }
        #endregion
    }
}
