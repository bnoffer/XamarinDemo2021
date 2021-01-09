// -----------------------------------------------------------------
//    Class:		App.xaml.cs
//    Description:	<Description>
//    Author:		Bastian Noffer <b.noffer@mac.com>	Date: 09.01.2021
//    Copyright:	©2021 Bastian Noffer
// -----------------------------------------------------------------

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinDemo2021.Services;
using XamarinDemo2021.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace XamarinDemo2021
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            AppCenter.Start($"ios={XamarinDemo2021.Resources.AppCenter.iOSAppSecret};" +
                            $"android={XamarinDemo2021.Resources.AppCenter.AndroidAppSecret}",
                            typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
