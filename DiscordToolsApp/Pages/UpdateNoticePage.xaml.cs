using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordToolsApp.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordToolsApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateNoticePage : ContentPage
    {
        public UpdateNoticePage()
        {
            InitializeComponent();
            lblUp.Text =
                @"My Discord Tools won't receive updates anymore. Since I wrote the application's foundation very poorly, I've decided to rewrite it from scratch. It will be released as a new app on the Google Play Store, and this current app will be removed. For more information, please contact us at our Discord address.";
        }

        private void btnDiscord_Clicked(object sender, EventArgs e)
        {
            References.discordClicked();

            if (Preferences.Get("privacy_policy_accepted10May2023", false))
                App.Current.MainPage = new NavigationPage(new MainPage())
                {
                    BarBackgroundColor = ThemeColors.StatusBarColor,
                    BarTextColor = ThemeColors.TextColor,
                };
            else
                App.Current.MainPage = new PrivacyPolicyPage();
        }

        private void btnSkip_Clicked(object sender, EventArgs e)
        {
            if (Preferences.Get("privacy_policy_accepted10May2023", false))
                App.Current.MainPage = new NavigationPage(new MainPage())
                {
                    BarBackgroundColor = ThemeColors.StatusBarColor,
                    BarTextColor = ThemeColors.TextColor,
                };
            else
                App.Current.MainPage = new PrivacyPolicyPage();
        }
    }
}
