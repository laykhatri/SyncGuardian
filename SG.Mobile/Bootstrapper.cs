using Microsoft.Extensions.DependencyInjection;
using SG.Mobile.ViewModels;
using SG.Mobile.Views;

namespace SG.Mobile
{
    internal class Bootstrapper
    {
        public static void ConfigureServices(IServiceCollection service)
        {
            // Views

            service.AddSingleton<MainView>();

            // ViewModels
            service.AddSingleton<MainViewModel>();
        }
    }
}
