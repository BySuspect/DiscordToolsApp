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
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            popupInfoBack.IsVisible = References.supportPopup;
        }
        private void DiscordButton_Clicked(object sender, EventArgs e)
        {
            Browser.OpenAsync("https://bit.ly/3NmBFDO", BrowserLaunchMode.SystemPreferred);
        }

        private async void btnTimestampGenerator_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TimestampGeneratorPage(), false);
        }
        private async void btnGetUserDetailsWithId_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new getUserDetailsPage(), false);
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        private void btnSupportCancel_Clicked(object sender, EventArgs e)
        {
            popupInfoBack.IsVisible = false;
            References.supportPopup = false;
        }
        private async void btnSupport_Clicked(object sender, EventArgs e)
        {
            await Browser.OpenAsync(new Uri("https://bit.ly/discordtoolspatreon"), BrowserLaunchMode.External);
            popupInfoBack.IsVisible = false;
            References.supportPopup = false;
        }
    }
}