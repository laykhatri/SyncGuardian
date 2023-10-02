using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SyncGuardianWpf.Services.Interfaces;
using SyncGuardianWpf.ViewModels;
using System;
using System.Windows.Controls;

namespace SyncGuardianWpf.Views
{
    /// <summary>
    /// Interaction logic for InitialSetupView.xaml
    /// </summary>
    public partial class InitialSetupView : Page
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        private InitialSetupViewModel _viewModel;

        public InitialSetupView(ILogger logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _viewModel = _serviceProvider.GetRequiredService<InitialSetupViewModel>();
            DataContext = _viewModel;
            InitializeComponent();
        }
    }
}
