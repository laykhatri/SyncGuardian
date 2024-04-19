using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SG.Server.Services.Interfaces;
using SG.Server.ViewModels;
using System;

namespace SG.Server.Views
{
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;
        private readonly IDeviceIDGenerationService _deviceIDGeneration;
        private MainWindowViewModel _viewModel;
        public MainWindow(IServiceProvider serviceProvider, ILogger logger, IDeviceIDGenerationService deviceIDGeneration)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _deviceIDGeneration = deviceIDGeneration;
            _viewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            DataContext = _viewModel;
            InitializeComponent();
            _logger.Information($"[{nameof(MainWindow)}]: Initialization finished.");
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            _viewModel.InitialSetupCheck();
        }
    }
}