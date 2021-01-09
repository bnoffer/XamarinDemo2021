using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinDemo2021.Services;
using XamarinDemo2021.Views;

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
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
