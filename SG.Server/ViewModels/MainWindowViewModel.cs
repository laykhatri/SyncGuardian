using ReactiveUI;

namespace SG.Server.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public IReactiveCommand _navigationCommand { get; }

        public MainWindowViewModel()
        {
            _navigationCommand = ReactiveCommand.Create<ViewModelBase>(NaviagteToPage);
        }

        private ViewModelBase _currentViewModel = default!;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                this.RaiseAndSetIfChanged(ref _currentViewModel, value);
            }
        }

        public void NaviagteToPage(ViewModelBase viewModel)
        {
            CurrentViewModel = viewModel;
        }
    }
}
