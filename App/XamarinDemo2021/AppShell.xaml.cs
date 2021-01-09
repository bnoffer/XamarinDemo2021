using System;
using System.Collections.Generic;
using XamarinDemo2021.ViewModels;
using XamarinDemo2021.Views;
using Xamarin.Forms;

namespace XamarinDemo2021
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
