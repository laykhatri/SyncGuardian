using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Hosting;

namespace SyncGuardian.Desktop;

public partial class App : Application
{
    private IHost? _host;
    public override void Initialize()
    {
        _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
        {
            Bootstrapper.RegisterServices(services);
        }).Build();
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        await _host!.StartAsync();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }

}