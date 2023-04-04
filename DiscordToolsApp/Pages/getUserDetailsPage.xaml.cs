using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Discord.WebSocket;
using Discord;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using Xamarin.Essentials;
using System.Diagnostics;

namespace DiscordToolsApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class getUserDetailsPage : ContentPage
    {
        private const string token = "MTA0NDU3ODAxMDcxMzU2NzI3Mg.GKEDx5.P3JrU74HlfRcBvSzxzszl5eWBrE8meBd3G4nRU";
        private HttpClient httpClient = new HttpClient();
        private DiscordSocketClient _client;
        public getUserDetailsPage()
        {
            InitializeComponent();
            _client = new DiscordSocketClient();
            startBot();
#if DEBUG
            //Clipboard.SetTextAsync("272665050672660501");
            //Clipboard.SetTextAsync("190916650143318016");
            //Clipboard.SetTextAsync("282859044593598464");
            Clipboard.SetTextAsync("901826325940154388");
#endif
        }
        async void startBot()
        {
            try
            {
                await _client.LoginAsync(TokenType.Bot, token);
                await _client.StartAsync();
            }
            catch (Exception ex)
            {
                _ = DisplayAlert("Error!", $"{ex.Message}", "Ok");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                ulong uID;
                try
                {
                    uID = ulong.Parse(entryUserID.Text);
                }
                catch
                {
                    _ = DisplayAlert("Warning!", "unknown id.", "Ok");
                    return;
                }


                // HTTP isteği oluşturun ve Discord API'den verileri çekin
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bot {token}");

                var user = await _client.GetUserAsync(uID);
                var response = await client.GetAsync($"https://discord.com/api/users/{uID}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                var userjson = JsonConvert.DeserializeObject<JObject>(result);

                var id = userjson["id"].ToString();
                var username = userjson["username"].ToString();
                var global_name = userjson["global_name"];
                var display_name = userjson["display_name"];
                var avatar = userjson["avatar"].ToString();
                var avatar_decoration = userjson["avatar_decoration"];
                var discriminator = userjson["discriminator"].ToString();
                var public_flags = userjson["public_flags"];
                var banner = userjson["banner"].ToString();
                var banner_color = userjson["banner_color"].ToString();
                var accent_color = userjson["accent_color"];

                var badges = await getUserBadges(user.PublicFlags.ToString().Split(new char[] { '|', ',' }).ToList());

                imgAvatar.Source = user.GetAvatarUrl().Split('?')[0] + "?size=4096";
                imgBanner.Source = $"https://cdn.discordapp.com/banners/{uID}/{banner}.gif?size=4096";
                lblUserName.Text = $"{username}#{discriminator}";
                lblCreationDate.Text = user.CreatedAt.ToString().Split('+')[0] + " UTC";
                lblUserID.Text = user.Id.ToString();
                lblBannerColor.Text = banner_color;
                lblBannerColor.BackgroundColor = Xamarin.Forms.Color.FromHex(banner_color);
            }
            catch (Exception ex)
            {
                _ = DisplayAlert("Error!", $"{ex.Message}", "Ok");
            }
        }
        async Task<List<string>> getUserBadges(List<string> flags)
        {
            foreach (var item in flags)
            {

            }

            return null;
        }
        //Discord.UserProperties.ActiveDeveloper
        //Discord.UserProperties.BotHTTPInteractionsDiscord.UserProperties.BugHunterLevel1
        //Discord.UserProperties.Bug HunterLevel2
        //Discord.UserProperties.DiscordCertifiedModerator
        //Discord.UserProperties.EarlySupporter
        //Discord.UserProperties.EarlyVerifiedBotDeveloper
        //Discord.UserProperties.HypeSquadBalance
        //Discord.UserProperties.HypeSquadBravery
        //Discord.UserProperties.HypeSquadBrilliance
        //Discord.UserProperties.HypeSquadEvents
        //Discord.UserProperties.None
        //Discord.UserProperties.Partner
        //Discord.UserProperties.Staff
        //Discord.UserProperties.System
        //Discord.UserProperties.TeamUser
        //Discord.UserProperties.VerifiedBot
    }
}