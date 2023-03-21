using DiscordToolsApp.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordToolsApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new NavigationPage(new TimestampGeneratorPage());
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
