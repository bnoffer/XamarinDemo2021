// -----------------------------------------------------------------
//    Class:		ListItemDataTemplate.xaml.cs
//    Description:	<Description>
//    Author:		Bastian Noffer <b.noffer@mac.com>	Date: 09.01.2021
//    Copyright:	©2021 Bastian Noffer
// -----------------------------------------------------------------
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamarinDemo2021.ViewModels;
using XamarinDemo2021.Shared.Models;

namespace XamarinDemo2021.Views
{
    public partial class ListItemView : ContentView
    {
        public ListItemView()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var item = BindingContext as Product;

            if (item.available)
            {
                LeftColumn.Width = new GridLength(100.0);
                RightColumn.Width = GridLength.Star;
                Grid.SetColumn(ItemImage, 0);
                Grid.SetColumn(ItemInformation, 1);
            }
            else
            {
                LeftColumn.Width = GridLength.Star;
                RightColumn.Width = new GridLength(100.0);
                Grid.SetColumn(ItemImage, 1);
                Grid.SetColumn(ItemInformation, 0);
            }
        }
    }
}
