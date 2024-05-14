using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using SG.Client.ViewModels;

namespace SG.Client.Views
{
    public partial class MainView : UserControl
    {
        private MainViewModel _viewModel;
        public MainView()
        {
            _viewModel = App.ServiceProvider.GetRequiredService<MainViewModel>();
            DataContext = _viewModel;
            InitializeComponent();
        }
    }
}