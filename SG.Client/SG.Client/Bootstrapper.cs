using Microsoft.Extensions.DependencyInjection;
using SG.Client.ViewModels;
using SG.Client.Views;

namespace SG.Client
{
    public static class Bootstrapper
    {
        public static void ConfigureServices(IServiceCollection serviceDescriptors)
        {
            #region Views
            serviceDescriptors.AddSingleton<MainView>();
            serviceDescriptors.AddSingleton<QrCodeReaderView>();
            #endregion

            #region ViweModels
            serviceDescriptors.AddSingleton<MainViewModel>();
            serviceDescriptors.AddSingleton<QrCodeReaderViewModel>();
            #endregion
        }
    }
}
