using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SG.Server.Services.Interfaces;
using SG.Server.ViewModels;

namespace SG.Server;

public partial class InitialSetupView : UserControl
{
    private readonly ILogger _logger;
    private readonly IDeviceIDGenerationService _deviceIDGeneration;
    private InitialSetupViewModel _viewModel;
    public InitialSetupView()
    {
        _logger = App.ServiceProvider!.GetRequiredService<ILogger>();
        _deviceIDGeneration = App.ServiceProvider!.GetRequiredService<IDeviceIDGenerationService>();
        _viewModel = App.ServiceProvider!.GetRequiredService<InitialSetupViewModel>();
        DataContext = _viewModel;
        InitializeComponent();
        _logger.Information($"[{nameof(InitialSetupView)}]: Initialization finished.");
    }
}