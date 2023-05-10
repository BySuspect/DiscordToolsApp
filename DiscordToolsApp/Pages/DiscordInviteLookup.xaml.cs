using DiscordToolsApp.Helpers;
using DiscordToolsApp.Pages.Popups;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            int testcounter = 0;
            try
            {
                var inviteCode = entryInviteLink.Text;
                entryInviteLink.Text = "";
                if (!string.IsNullOrEmpty(inviteCode))
                {
                    testcounter++;//1
                    Loodinglayout.IsVisible = true;
                    InviterDetailsView.IsVisible = false;
                    inviterView.IsVisible = false;
                    var client = new HttpClient();
                    var doc = new HtmlDocument();

                    testcounter++;//2
                    //Api request
                    client.DefaultRequestHeaders.Add("Authorization", $"Bot {token}");
                    string code = "";
                    if (inviteCode.Contains('/'))
                    {
                        string[] parts = inviteCode.Split('/');
                        code = parts.LastOrDefault(p => !string.IsNullOrWhiteSpace(p));
                    }
                    else
                        code = inviteCode;
                    testcounter++;//3
                    var apires = await client.GetAsync($"https://discord.com/api/invites/{code}?with_counts=true&with_expiration=true");
                    var rescode = apires.EnsureSuccessStatusCode();
                    if (rescode.IsSuccessStatusCode)
                    {
                        testcounter++;//4
                        var result = await apires.Content.ReadAsStringAsync();
                        testcounter++;//5
                        var invitedata = JsonConvert.DeserializeObject<JObject>(result);
                        testcounter++;//6

                        DateTime expiresAt = (DateTime)invitedata["expires_at"];
                        string guildId = (string)invitedata["guild"]["id"];
                        string guildName = (string)invitedata["guild"]["name"];
                        string guildDesc = (string)invitedata["guild"]["description"];
                        string guildIcon = (string)invitedata["guild"]["icon"];
                        JArray featuresArray = (JArray)invitedata["guild"]["features"];
                        string[] guildFeatures = featuresArray.ToObject<string[]>();
                        int guildVerifyLevel = (int)invitedata["guild"]["verification_level"];
                        int guildNitroCount = (int)invitedata["guild"]["premium_subscription_count"];
                        bool guildNSFW = (bool)invitedata["guild"]["nsfw"];
                        int guildNSFWLevel = (int)invitedata["guild"]["nsfw_level"];
                        int guilMemberCount = (int)invitedata["approximate_member_count"];
                        int guilOnlineMemberCount = (int)invitedata["approximate_presence_count"];

                        testcounter++;//7
                        string inviterId = (string)invitedata["inviter"]["id"];
                        string inviterName = (string)invitedata["inviter"]["username"];
                        string inviterAvatar = (string)invitedata["inviter"]["avatar"];
                        string inviterDiscriminator = (string)invitedata["inviter"]["discriminator"];


                        //Server Info
                        imgGuildIcon.Source = $"https://cdn.discordapp.com/icons/{guildId}/{guildIcon}?size=256";
                        //var guildSplash = $"https://cdn.discordapp.com/splashes/{invitedata.guild.id}/{invitedata.guild.splash}?size=256";
                        lblInviteLink.Text = $"https://discord.com/invite/{code}";
                        lblExpress.Text = (expiresAt != null) ? ((int)CalculateRemainingTime(expiresAt).TotalDays).ToString() + " Days" : "Unlimited";
                        lblGuildId.Text = guildId;
                        lblGuildName.Text = guildName;
                        lblGuildDesc.Text = guildDesc;
                        lblGuildFeatures.Text = string.Join(", ", guildFeatures);
                        lblGuildVerfLevel.Text = guildVerifyLevel.ToString();
                        lblGuildMemberCount.Text = guilMemberCount.ToString();
                        lblGuildOnlineMemberCount.Text = guilOnlineMemberCount.ToString();
                        lblGuildNitroCount.Text = guildNitroCount.ToString();
                        lblGuildNSFW.Text = guildNSFW.ToString();
                        lblGuildNSFWLevel.Text = guildNSFWLevel.ToString();

                        testcounter++;//8
                        //Inviter Info
                        if (!string.IsNullOrEmpty(inviterId))
                        {
                            imgAvatar.Source = $"https://cdn.discordapp.com/avatars/{inviterId}/{inviterAvatar}?size=256";
                            lblUserID.Text = inviterId.ToString();
                            lblUserName.Text = $"{inviterName}#{inviterDiscriminator}";

                            inviterView.IsVisible = true;
                            testcounter++;//9
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
                await DisplayAlert("Something went wrong try again later. Error Code: " + testcounter, ex.Message, "Ok");
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
    }
}