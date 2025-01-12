using Microsoft.Extensions.DependencyInjection;
using SG.DesktopWeb.ViewModels;
using SG.DesktopWeb.Views;

namespace SG.DesktopWeb
{
    internal class Boostrapper
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
