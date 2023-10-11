using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SyncGuardianWpf.Services;
using SyncGuardianWpf.Services.Interfaces;
using SyncGuardianWpf.ViewModels;
using SyncGuardianWpf.Views;
using System.IO;

namespace SyncGuardianWpf
{
    public static class DiConfig
    {
        public static void ConfigureDependencies(IServiceCollection services)
        {
            // Logger Service
            var logFilePath = "Logs\\SyncGuardian-.txt";
            EnsureDirectoryExists(logFilePath);

            Serilog.ILogger logger = new LoggerConfiguration().MinimumLevel.Information()
                .WriteTo.File(logFilePath, fileSizeLimitBytes: 10 * 1024 * 1024, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 10, retainedFileTimeLimit: System.TimeSpan.FromDays(7))
                .CreateLogger();
            services.AddSingleton(logger);

            // MainWindow DI
            services.AddSingleton<MainWindow>();
            services.AddSingleton<SplashWindow>();
            services.AddSingleton<InitialSetupView>();

            // ViewModel
            services.AddSingleton<InitialSetupViewModel>();

            // OtherServiceDI
            services.AddTransient<IDeviceIDGenerationService,DeviceIDGenerationService>();
            services.AddTransient<IQRCodeGenerationService,QRCodeGenerationService>();
            services.AddTransient<INetworkInterfaceService,NetworkInterfaceService>();
            services.AddTransient<IAssemblyVersionInfo,AssemblyVersionInfo>();
        }

        private static void EnsureDirectoryExists(string filePath)
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
