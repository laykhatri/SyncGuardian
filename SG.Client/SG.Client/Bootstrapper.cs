using Microsoft.Extensions.DependencyInjection;
using SG.Client.Services.Implementations;
using SG.Client.Services.Interfaces;
using SG.Client.ViewModels;
using SG.Client.Views;

namespace SG.Client
{
    internal class Bootstrapper
    {
        internal static void ConfigureServices(IServiceCollection services)
        {
            #region Views
            services.AddSingleton<MainView>();
            services.AddSingleton<InitialSetupTempView>();
            #endregion

            #region ViewModels
            services.AddSingleton<MainViewModel
                >();
            services.AddSingleton<InitialSetupTempViewModel>();
            #endregion

            #region Services
            services.AddSingleton<IFileService, FileService>();
            #endregion
        }
    }
}
