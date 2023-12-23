using Microsoft.Extensions.DependencyInjection;
using SyncGuardianMobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SyncGuardianMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InitialSetupPage : ContentPage
    {
        private readonly IServiceProvider _serviceProvider;
        private InitialSetupViewModel _viewModel;
        public InitialSetupPage(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _viewModel = _serviceProvider.GetRequiredService<InitialSetupViewModel>();
            BindingContext = _viewModel;

            InitializeComponent();
        }
    }
}