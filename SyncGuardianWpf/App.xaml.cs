using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SyncGuardianWpf
{
    public partial class App : Application
    {
        private IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // DI binding here
                    DiConfig.ConfigureDependencies(services);
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Start Hosting service
            await _host.StartAsync();

            // Initialize MainWindow
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            
            // Display MainWindow
            mainWindow.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            // Stop Hosting service
            await _host.StopAsync();
        }
    }
}
