using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SyncGuardianWpf.Services;
using SyncGuardianWpf.Services.Interfaces;
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

            // OtherServiceDI
            services.AddSingleton<IDeviceIDGenerationService,DeviceIDGenerationService>();
            services.AddSingleton<IQRCodeGenerationService,QRCodeGenerationService>();
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
