using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SG.Server.Services.Interfaces;
using SG.Server.ViewModels;
using System;

namespace SG.Server;

public partial class InitialSetupView : UserControl
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;
    private readonly IDeviceIDGenerationService _deviceIDGeneration;
    private InitialSetupViewModel _viewModel;
    public InitialSetupView(IServiceProvider serviceProvider, ILogger logger, IDeviceIDGenerationService deviceIDGeneration)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _deviceIDGeneration = deviceIDGeneration;
        _viewModel = _serviceProvider.GetRequiredService<InitialSetupViewModel>();
        DataContext = _viewModel;
        InitializeComponent();
        _logger.Information($"[{nameof(InitialSetupView)}]: Initialization finished.");
    }
}