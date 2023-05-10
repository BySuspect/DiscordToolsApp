using DiscordToolsApp.Helpers;
using DiscordToolsApp.Pages.Popups;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
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
#if DEBUG
            entryInviteLink.Text = "XRPVZEZt";
#endif
        }
        private async void btnLookup_Clicked(object sender, EventArgs e)
        {
            try
            {
                var inviteCode = entryInviteLink.Text;
                entryInviteLink.Text = "";
                if (!string.IsNullOrEmpty(inviteCode))
                {
                    Loodinglayout.IsVisible = true;
                    InviterDetailsView.IsVisible = false;
                    inviterView.IsVisible = false;
                    var client = new HttpClient();
                    var doc = new HtmlDocument();


                    //Api request
                    client.DefaultRequestHeaders.Add("Authorization", $"Bot {token}");
                    string code = "";
                    if (true)
                    {
                        string[] parts = inviteCode.Split('/');
                        code = parts.LastOrDefault(p => !string.IsNullOrWhiteSpace(p));
                    }
                    var apires = await client.GetAsync($"https://discord.com/api/invites/{code}?with_counts=true&with_expiration=true");
                    var rescode = apires.EnsureSuccessStatusCode();
                    if (rescode.IsSuccessStatusCode)
                    {
                        var result = await apires.Content.ReadAsStringAsync();
                        var invitedata = JsonConvert.DeserializeObject<InviteBase>(result);

                        //Server Info
                        imgGuildIcon.Source = $"https://cdn.discordapp.com/icons/{invitedata.guild.id}/{invitedata.guild.icon}?size=256";
                        var guildSplash = $"https://cdn.discordapp.com/splashes/{invitedata.guild.id}/{invitedata.guild.splash}?size=256";
                        lblInviteLink.Text = $"https://discord.com/invite/{invitedata.code}";
                        lblExpress.Text = (invitedata.expires_at != null) ? CalculateRemainingTime((DateTime)invitedata.expires_at).ToString() : "Unlimited";
                        lblGuildId.Text = invitedata.guild.id;
                        lblGuildName.Text = invitedata.guild.name;
                        lblGuildDesc.Text = invitedata.guild.description;
                        lblGuildFeatures.Text = string.Join(", ", invitedata.guild.features);
                        lblGuildVerfLevel.Text = invitedata.guild.verification_level.ToString();
                        lblGuildMemberCount.Text = invitedata.approximate_member_count.ToString();
                        lblGuildOnlineMemberCount.Text = invitedata.approximate_presence_count.ToString();
                        lblGuildNitroCount.Text = invitedata.guild.premium_subscription_count.ToString();
                        lblGuildNSFW.Text = invitedata.guild.nsfw.ToString();
                        lblGuildNSFWLevel.Text = invitedata.guild.nsfw_level.ToString();

                        //Inviter Info
                        if (invitedata.inviter != null)
                        {
                            var inviter = invitedata.inviter;
                            imgAvatar.Source = $"https://cdn.discordapp.com/avatars/{inviter.id}/{inviter.avatar}?size=256";
                            lblUserName.Text = $"{inviter.username}#{inviter.discriminator}";
                            lblUserID.Text = inviter.id.ToString();

                            inviterView.IsVisible = true;
                        }

                        while (true)
                        {
                            if (!(imgAvatar.IsLoading || imgGuildIcon.IsLoading))
                            {
                                Loodinglayout.IsVisible = false;
                                break;
                            }
                            else await Task.Delay(300);
                        }

                        //var jsonObject = JsonConvert.DeserializeObject(result);
                        //testlbl.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                        //testbg.IsVisible = true;
                    }
                    else
                    {
                        throw new Exception("Api request error!");
                    }
                    Loodinglayout.IsVisible = false;
                    InviterDetailsView.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Something went wrong try again later.", ex.Message, "Ok");
                InviterDetailsView.IsVisible = false;
            }
            Loodinglayout.IsVisible = false;
        }
        private async void lblInviteLink_Tapped(object sender, EventArgs e)
        {
            await Browser.OpenAsync(lblInviteLink.Text);
        }
        private async void Avatar_Tapped(object sender, EventArgs e)
        {
            var uriImageSource = imgAvatar.Source as UriImageSource;
            if (uriImageSource != null)
            {
                await Clipboard.SetTextAsync(uriImageSource.Uri.AbsoluteUri.ToString());
                ToastController.ShowShortToast("Avatar Link Copied!");
            }
        }
        private async void GuildIcon_Tapped(object sender, EventArgs e)
        {
            var uriImageSource = imgGuildIcon.Source as UriImageSource;
            if (uriImageSource != null)
            {
                await Clipboard.SetTextAsync(uriImageSource.Uri.AbsoluteUri.ToString());
                ToastController.ShowShortToast("Guild Icon Link Copied!");
            }
        }

        public TimeSpan CalculateRemainingTime(DateTime targetDateTime)
        {
            DateTime now = DateTime.Now;
            TimeSpan remainingTime = targetDateTime - now;

            return remainingTime;
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
                public bool? nsfw { get; set; }
                public int? nsfw_level { get; set; }
                public int? premium_subscription_count { get; set; }
            }
            public class User
            {
                public string id { get; set; }
                public string username { get; set; }
                public object global_name { get; set; }
                public object display_name { get; set; }
                public string avatar { get; set; }
                public string discriminator { get; set; }
                public int public_flags { get; set; }
                public string avatar_decoration { get; set; }
            }
        }
    }
}