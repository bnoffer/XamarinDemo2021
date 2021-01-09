using System.ComponentModel;
using Xamarin.Forms;
using XamarinDemo2021.ViewModels;

namespace XamarinDemo2021.Views
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