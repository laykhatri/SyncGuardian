using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SG.Server.Services.Interfaces;
using SG.Server.ViewModels;

namespace SG.Server.Views
{
    public partial class MainWindow : Window
    {
        private readonly ILogger _logger;
        private readonly IDeviceIDGenerationService _deviceIDGeneration;
        private MainWindowViewModel _viewModel;
        public MainWindow()
        {
            _logger = App.ServiceProvider.GetRequiredService<ILogger>();
            _deviceIDGeneration = App.ServiceProvider.GetRequiredService<IDeviceIDGenerationService>();
            _viewModel = App.ServiceProvider.GetRequiredService<MainWindowViewModel>();
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