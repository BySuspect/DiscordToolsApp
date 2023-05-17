using DiscordToolsApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordToolsApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrivacyPolicyPage : ContentPage
    {
        public PrivacyPolicyPage()
        {
            InitializeComponent();
            lblUp.Text = @"Hello,

Before you start using our application, we kindly ask you to read and accept our privacy policy. Our privacy policy provides detailed information on how we collect, use, and protect your personal data.

Please click on the link below to review and accept our privacy policy:" + "\n\n";
            lblDown.Text = "\n\n" + @"Continuing to use our application signifies that you have read and agreed to our privacy policy. We appreciate your understanding of the importance we place on privacy and data security.

If you have any questions, please don't hesitate to contact us.

Best regards,
DiscordTools Team";
        }
        private void btnPrivacyPolicyAccept(object sender, EventArgs e)
        {
            // Save preference indicating that the user has accepted the privacy policy
            Preferences.Set("privacy_policy_accepted10May2023", true);
            btnaccept.IsEnabled = false;
            App.Current.MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = ThemeColors.StatusBarColor,
                BarTextColor = ThemeColors.TextColor,
            };
        }

        private async void PrivacyPolicy_Tapped(object sender, EventArgs e)
        {
            await Browser.OpenAsync("https://awgstudiosapps.web.app/discordtools/privacy-policy.html");
        }
    }
}