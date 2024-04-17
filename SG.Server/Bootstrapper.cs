using Microsoft.Extensions.DependencyInjection;
using SG.Server.ViewModels;

namespace SG.Server
{
    internal class Bootstrapper
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // ViewModels
            services.AddSingleton<MainWindowViewModel>();

            // Services
        }
    }
}
