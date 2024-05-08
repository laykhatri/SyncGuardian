using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SG.Server.Services.Interfaces;
using SG.Server.Views;

namespace SG.Server
{
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; } = null!;

        private static IWebService? _webService = null;
        public App()
        {
            var serviceCollection = new ServiceCollection();
            Bootstrapper.ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            _webService = ServiceProvider!.GetRequiredService<IWebService>();
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                _webService?.Start();
                desktop.MainWindow = ServiceProvider!.GetRequiredService<MainWindow>();
                desktop.Exit += OnExit;
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void OnExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
        {
            _webService?.Stop();
        }
    }
}