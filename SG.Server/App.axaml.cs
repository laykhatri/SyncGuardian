using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SG.Server.ViewModels;
using SG.Server.Views;

namespace SG.Server
{
    public partial class App : Application
    {
        private ServiceProvider? _serviceProvider;
        public override void Initialize()
        {
            var serviceCollection = new ServiceCollection();
            Bootstrapper.ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = _serviceProvider?.GetRequiredService<MainWindowViewModel>()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}