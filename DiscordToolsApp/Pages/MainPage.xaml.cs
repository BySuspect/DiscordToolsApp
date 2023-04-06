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
        }
        private async void DiscordButton_Clicked(object sender, EventArgs e)
        {
            await Browser.OpenAsync("https://discord.gg/aX4unxzZek");
        }

        private void btnTimestampGenerator_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TimestampGeneratorPage(), false);
        }
        private void btnGetUserDetailsWithId_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TimestampGeneratorPage(), false);
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}