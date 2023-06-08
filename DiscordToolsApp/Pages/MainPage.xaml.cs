﻿using DiscordToolsApp.Helpers;
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
        private async void btnGetUserDetailsWithId_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new getUserDetailsPage(), false);
        }
        private async void btnTextToEmoji_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TextToDiscordEoji(), false);
        }
        private async void btnInviteLookUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DiscordInviteLookup(), false);
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
            Browser.OpenAsync("https://bit.ly/3NmBFDO", BrowserLaunchMode.SystemPreferred);
        }
        private async void FeedbackButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                Popup popup = new FeedbackPopupPage();
                var res = await App.Current.MainPage.Navigation.ShowPopupAsync(popup);
                if (res.ToString() == "counterror")
                {
                    await DisplayAlert("Warning!", "You reached daily feedback limit.", "Ok");
                }
                else if (res.ToString() == "catcherror")
                {
                    await DisplayAlert("Error!", "Something went wrong try again later.", "Ok");
                }
            }
            catch
            {

            }
        }
    }
}