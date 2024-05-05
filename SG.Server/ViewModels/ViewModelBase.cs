using Avalonia.Controls;
using ReactiveUI;

namespace SG.Server.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        private UserControl? _currentPage;
        public UserControl? CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
                this.RaisePropertyChanged(nameof(CurrentPage));
            }
        }

    }
}
