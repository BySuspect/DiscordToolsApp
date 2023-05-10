using DiscordToolsApp.Pages.Popups;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
    public partial class DiscordInviteLookup : ContentPage
    {
        private const string token = "MTA0NDU3ODAxMDcxMzU2NzI3Mg.G98N5e.Wz5mtT6sf4g5-noSeda0Cv2HxSpWIw5zaBXRVQ";
        public DiscordInviteLookup()
        {
            InitializeComponent();
            entryInviteLink.Text = "XRPVZEZt";
        }
        private async void btnLookup_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(entryInviteLink.Text))
                {
                    var client = new HttpClient();
                    var doc = new HtmlDocument();


                    //Api request
                    client.DefaultRequestHeaders.Add("Authorization", $"Bot {token}");
                    string code = "";
                    if (true)
                    {
                        string[] parts = entryInviteLink.Text.Split('/');
                        code = parts.LastOrDefault(p => !string.IsNullOrWhiteSpace(p));
                    }
                    var apires = await client.GetAsync($"https://discord.com/api/invites/{code}?with_counts=true&with_expiration=true");
                    var rescode = apires.EnsureSuccessStatusCode();
                    if (rescode.IsSuccessStatusCode)
                    {
                        var result = await apires.Content.ReadAsStringAsync();
                        var userjson = JsonConvert.DeserializeObject<InviteBase>(result);

                        //var jsonObject = JsonConvert.DeserializeObject(result);
                        //testlbl.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                        //testbg.IsVisible = true;


                    }
                    else
                    {
                        throw new Exception("Api request error!");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Something went wrong try again later.", ex.Message, "Ok");
            }



        }
        private async void lblInviteLink_Tapped(object sender, EventArgs e)
        {
            await Browser.OpenAsync(lblInviteLink.Text);
        }

        private async void DiscordButton_Clicked(object sender, EventArgs e)
        {
            await Browser.OpenAsync("https://bit.ly/3NmBFDO");
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
        public class InviteBase
        {
            public string code { get; set; }
            public Guild guild { get; set; }
            public Channel? channel { get; set; }
            public User? inviter { get; set; }
            public int? target_type { get; set; }
            public User? target_user { get; set; }
            public int? approximate_presence_count { get; set; }
            public int? approximate_member_count { get; set; }
            public DateTime? expires_at { get; set; }

            public class Channel
            {
                public string id { get; set; }
                public string name { get; set; }
                public int? type { get; set; }
            }
            public class Guild
            {
                public string id { get; set; }
                public string name { get; set; }
                public object? splash { get; set; }
                public object? banner { get; set; }
                public string? description { get; set; }
                public object? icon { get; set; }
                public List<string>? features { get; set; }
                public int? verification_level { get; set; }
                public object? vanity_url_code { get; set; }
                public int? nsfw_level { get; set; }
                public int? premium_subscription_count { get; set; }
            }
            public class User
            {
                public ulong Id { get; set; }
                public string Username { get; set; }
                public string Discriminator { get; set; }
                public string? Avatar { get; set; }
                public bool? Bot { get; set; }
                public bool? System { get; set; }
                public bool? MfaEnabled { get; set; }
                public string? Locale { get; set; }
                public bool? Verified { get; set; }
                public string? Email { get; set; }
                public int? Flags { get; set; }
                public int? PremiumType { get; set; }
                public int? PublicFlags { get; set; }
            }
        }
    }
}