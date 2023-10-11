using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace SyncGuardianWpf
{
    public partial class App : Application
    {
        private IHost _host;
        private SplashWindow _splashWindow;
        private Stopwatch _timer;
        public App()
        {
            // Initialize Stopwatch and start
            _timer = new Stopwatch();
            _timer.Start();

            // Initialize SplashWindow
            _splashWindow = new SplashWindow();
            _splashWindow.Show();

            // Start configuring Host service
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

            // Stop Stopwatch
            _timer.Stop();

            // Calculate Remaining time for the 3 seconds
            var remainingDelay = 3000 - (int)_timer.ElapsedMilliseconds;
            if (remainingDelay > 0)
                await Task.Delay(remainingDelay);

            // Close SplashScreen when MainWindow is ready
            _splashWindow.Close();

            // Display MainWindow
            mainWindow.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            // Stop Hosting service
            await _host.WaitForShutdownAsync();
        }
    }
}
