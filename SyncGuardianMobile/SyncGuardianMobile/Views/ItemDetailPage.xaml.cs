using SyncGuardianMobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace SyncGuardianMobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}