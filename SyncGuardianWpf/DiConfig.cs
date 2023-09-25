using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncGuardianWpf
{
    public static class DiConfig
    {
        public static void ConfigureDependencies(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
        }
    }
}
