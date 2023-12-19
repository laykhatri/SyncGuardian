using Android.Util;
using Microsoft.Extensions.DependencyInjection;
using SyncGuardianMobile.Services;
using SyncGuardianMobile.Services.Interface;
using SyncGuardianMobile.ViewModels;
using SyncGuardianMobile.Views;

namespace SyncGuardianMobile.Droid
{
    public static class Configure
    {
        public static ServiceProvider DI()
        {
            var servicesCollection = new ServiceCollection();

            // Services
            servicesCollection.AddTransient<IDeviceIDGenerator,DeviceIDGenerator>();
            servicesCollection.AddTransient<IAssemblyVersionInfo,AssemblyVersionInfo>();
            servicesCollection.AddTransient<Log>();

            // Pages
            servicesCollection.AddSingleton<AppShell>();
            servicesCollection.AddSingleton<InitialSetupPage>();

            // ViewModels
            servicesCollection.AddSingleton<InitialSetupViewModel>();

            return servicesCollection.BuildServiceProvider();
        }
    }
}