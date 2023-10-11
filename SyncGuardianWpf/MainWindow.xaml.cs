using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SyncGuardianWpf.Services.Interfaces;
using SyncGuardianWpf.Views;
using System;
using System.Windows;

namespace SyncGuardianWpf
{
    public partial class MainWindow : Window
    {
        private readonly ILogger _logger;
        private readonly IDeviceIDGenerationService _deviceIDGeneration;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAssemblyVersionInfo _assemblyVersionInfo;

        public MainWindow(ILogger logger, IDeviceIDGenerationService deviceIDGeneration, IServiceProvider serviceProvider, IAssemblyVersionInfo assemblyVersionInfo)
        {
            _logger = logger;
            _deviceIDGeneration = deviceIDGeneration;
            _serviceProvider = serviceProvider;
            _assemblyVersionInfo = assemblyVersionInfo;
            InitializeComponent();
        }

        private void Redirect()
        {
            if(_deviceIDGeneration.NeedInitialSetup())
            { 
                var InitialSetupView = _serviceProvider.GetRequiredService<InitialSetupView>();
                MainFrame.Navigate(InitialSetupView);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Redirect();
        }
    }
}
