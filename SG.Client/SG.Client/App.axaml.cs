using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SG.Client.Views;

namespace SG.Client
{
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; } = null!;

        public static Control? MainView { get; set; }

        public App()
        {
            var serviceCollection = new ServiceCollection();
            Bootstrapper.ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                MainView = singleViewPlatform.MainView = ServiceProvider.GetRequiredService<MainView>();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}