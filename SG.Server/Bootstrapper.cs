using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SG.Server.Services.Implementations;
using SG.Server.Services.Interfaces;
using SG.Server.ViewModels;
using SG.Server.Views;
using System.IO;

namespace SG.Server
{
    internal class Bootstrapper
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            #region Logger Service
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
            #endregion

            #region Windows / Views
            services.AddSingleton<MainWindow>();
            services.AddSingleton<InitialSetupView>();
            services.AddSingleton<HomePageView>();
            #endregion

            #region ViewModels
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<InitialSetupViewModel>();
            services.AddSingleton<HomePageViewModel>();
            #endregion

            #region Services
            services.AddSingleton<IAssemblyVersionInfo, AssemblyVersionInfo>();
            services.AddSingleton<INetworkInterfaceService, NetworkInterfaceService>();
            services.AddSingleton<IDeviceIDGenerationService, DeviceIDGenerationService>();
            services.AddSingleton<IQRCodeGenerationService, QRCodeGenerationService>();
            services.AddSingleton<IWebService, WebService>();
            #endregion

        }
    }
}
