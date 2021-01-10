// -----------------------------------------------------------------
//    Class:		StarImageView.xaml.cs
//    Description:	<Description>
//    Author:		Bastian Noffer <b.noffer@mac.com>	Date: 10.01.2021
//    Copyright:	©2021 Bastian Noffer
// -----------------------------------------------------------------

using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Microsoft.AppCenter.Crashes;

namespace XamarinDemo2021.Views
{
    public partial class StarImageView : ContentView
    {
        public StarImageView()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            try
            {
                base.OnBindingContextChanged();

                var filled = (bool)BindingContext;

                if (filled)
                {
                    var dict = new Dictionary<string, string>();
                    dict.Add("fill:#4D4D4D", $"fill:#f2ca3c");
                    StarImage.ReplaceStringMap = dict;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}
