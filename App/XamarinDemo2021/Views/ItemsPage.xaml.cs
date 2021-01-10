// -----------------------------------------------------------------
//    Class:		ItemsPage.xaml.cs
//    Description:	<Description>
//    Author:		Bastian Noffer <b.noffer@mac.com>	Date: 10.01.2021
//    Copyright:	©2021 Bastian Noffer
// -----------------------------------------------------------------

using Xamarin.Forms;
using XamarinDemo2021.ViewModels;

namespace XamarinDemo2021.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.OnDisappearing();
        }
    }
}