using Microsoft.Extensions.DependencyInjection;
using SyncGuardianMobile.Droid;
using SyncGuardianMobile.Services.Interface;
using SyncGuardianMobile.Views;
using System;
using Xamarin.Forms;

namespace SyncGuardianMobile
{
    public partial class App : Application
    {
        public static IServiceProvider _serviceProvider { get; private set; }

        public App()
        {
            InitializeComponent();

            _serviceProvider = Configure.DI();
        }

        protected override void OnStart()
        {
            var _deviceId = _serviceProvider.GetService<IDeviceIDGenerator>();

            if(_deviceId != null && _deviceId.NeedInitialSetup() )
            {
                MainPage = _serviceProvider.GetRequiredService<InitialSetupPage>();
            }
            else
            {
                MainPage = _serviceProvider.GetRequiredService<AppShell>();
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
