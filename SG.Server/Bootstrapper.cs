using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SG.Server.Services.Implementations;
using SG.Server.Services.Interfaces;
using SG.Server.ViewModels;
using System.IO;

namespace SG.Server
{
    internal class Bootstrapper
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // Logger Service
            var logFilePath = "Logs\\SyncGuardian-.txt";

            var directory = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            ILogger logger = new LoggerConfiguration().MinimumLevel.Information()
                .WriteTo.File(logFilePath, fileSizeLimitBytes: 10 * 1024 * 1024, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 10, retainedFileTimeLimit: System.TimeSpan.FromDays(7))
                .CreateLogger();
            services.AddSingleton(logger);

            // ViewModels
            services.AddSingleton<MainWindowViewModel>();

            // Services
            services.AddSingleton<IAssemblyVersionInfo, AssemblyVersionInfo>();
            services.AddSingleton<INetworkInterfaceService, NetworkInterfaceService>();
        }
    }
}
