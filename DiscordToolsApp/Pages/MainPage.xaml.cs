using DiscordToolsApp.Helpers;
using DiscordToolsApp.Pages.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordToolsApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            var savedDate = Preferences.Get("SupportPopupDate", DateTime.MinValue);
            if (savedDate.Date != DateTime.Now.Date)
                popupInfoBack.IsVisible = true;
        }
        protected override void OnAppearing()
        {
            _ = References.CheckAppVersion();
            base.OnAppearing();
        }

        private async void btnTimestampGenerator_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TimestampGeneratorPage(), false);
        }
        private async void btnTextToEmoji_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TextToDiscordEoji(), false);
        }
        private async void btnInviteLookUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DiscordInviteLookup(), false);
        }
        private async void btnGetUserDetailsWithId_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new getUserDetailsPage(), false);
        }
        private async void btnSimpleWebhookMessage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SimpleWebhookSenderPage(), false);
        }


        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        private void btnSupportCancel_Clicked(object sender, EventArgs e)
        {
            popupInfoBack.IsVisible = false;
            Preferences.Set("SupportPopupDate", DateTime.Now);
        }
        private async void btnSupport_Clicked(object sender, EventArgs e)
        {
            await Browser.OpenAsync(new Uri("https://bit.ly/3pR7H0W"), BrowserLaunchMode.External);
            popupInfoBack.IsVisible = false;
            Preferences.Set("SupportPopupDate", DateTime.Now);
        }
        private void DiscordButton_Clicked(object sender, EventArgs e)
        {
            References.discordClicked();
        }
        private void FeedbackButton_Clicked(object sender, EventArgs e)
        {
            References.FeedbackClicked();
        }
    }
}