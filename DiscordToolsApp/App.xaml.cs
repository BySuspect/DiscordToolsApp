using DiscordToolsApp.Helpers;
using DiscordToolsApp.Pages;
using DiscordToolsApp.Pages.Popups;
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
#if !DEBUG
            if (Preferences.Get("privacy_policy_accepted10May2023", false))
                MainPage = new NavigationPage(new MainPage())
                {
                    BarBackgroundColor = ThemeColors.StatusBarColor,
                    BarTextColor = ThemeColors.TextColor,
                };
            else
                MainPage = new PrivacyPolicyPage();
#endif
#if DEBUG
            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = ThemeColors.StatusBarColor,
                BarTextColor = ThemeColors.TextColor,
            };
#endif


            //MainPage = new PrivacyPolicyPage();


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
