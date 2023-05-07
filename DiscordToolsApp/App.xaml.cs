using DiscordToolsApp.Helpers;
using DiscordToolsApp.Pages;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordToolsApp
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();

            switch (AppInfo.RequestedTheme)
            {
                case AppTheme.Unspecified:
                    ChangeAppTheme.DarkTheme();
                    break;
                case AppTheme.Light:
                    ChangeAppTheme.LightTheme();
                    break;
                case AppTheme.Dark:
                    ChangeAppTheme.DarkTheme();
                    break;
                default:
                    break;
            }
            if (Preferences.Get("{zenandshriokossecret}", false))
                ChangeAppTheme.ForDenizTheme();

            //ChangeAppTheme.ForDenizTheme();

            //MainPage = new NavigationPage(new MainPage())
            //{
            //    BarBackgroundColor = ThemeColors.StatusBarColor,
            //    BarTextColor = ThemeColors.TextColor,
            //};

            MainPage = new NavigationPage(new DiscordInviteLookup())
            {
                BarBackgroundColor = ThemeColors.StatusBarColor,
                BarTextColor = ThemeColors.TextColor,
            };
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
