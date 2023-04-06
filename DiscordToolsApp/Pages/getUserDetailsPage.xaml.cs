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
using static System.Net.Mime.MediaTypeNames;
using System.Collections.ObjectModel;
using DiscordToolsApp.Helpers;

namespace DiscordToolsApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class getUserDetailsPage : ContentPage
    {
        private ObservableCollection<badgeItems> badgeList = new ObservableCollection<badgeItems>();
        private const string token = "MTA0NDU3ODAxMDcxMzU2NzI3Mg.GKEDx5.P3JrU74HlfRcBvSzxzszl5eWBrE8meBd3G4nRU";
        private HttpClient httpClient = new HttpClient();
        private DiscordSocketClient _client;
        public getUserDetailsPage()
        {
            InitializeComponent();
            BindingContext = this;
            _client = new DiscordSocketClient();
            startBot();
            BindableLayout.SetItemsSource(badgesView, badgeList);


#if DEBUG
            Clipboard.SetTextAsync("272665050672660501");
            //Clipboard.SetTextAsync("190916650143318016");
            //Clipboard.SetTextAsync("282859044593598464");
            //Clipboard.SetTextAsync("901826325940154388");
#endif

        }
        async void startBot()
        {
            Loodinglayout.IsVisible = true;
            try
            {
                await _client.LoginAsync(TokenType.Bot, token);
                await _client.StartAsync();
            }
            catch (Exception ex)
            {
                _ = DisplayAlert("Error!", $"{ex.Message}", "Ok");
            }
            Loodinglayout.IsVisible = false;
        }

        private async void getUserButton_Clicked(object sender, EventArgs e)
        {
            #region easterEgg
            if (entryUserID.Text == "{zenandshriokossecret}")
            {
                if (!Preferences.Get("{zenandshriokossecret}", false))
                {
                    Preferences.Set("{zenandshriokossecret}", true);
                    ChangeAppTheme.ForDenizTheme();
                }
                else
                {
                    Preferences.Set("{zenandshriokossecret}", false);
                    switch (AppInfo.RequestedTheme)
                    {
                        case AppTheme.Unspecified:
                            ChangeAppTheme.DarkTheme();
                            break;
                        case AppTheme.Light:
                            ChangeAppTheme.LightTheme();
                            break;
                        case AppTheme.Dark:
                            ChangeAppTheme.DarkTheme();
                            break;
                        default:
                            break;
                    }
                }
                App.Current.MainPage = new NavigationPage(new getUserDetailsPage())
                {
                    BarBackgroundColor = ThemeColors.BackColor,
                    BarTextColor = ThemeColors.TextColor,
                };
                return;
            }
            #endregion

            badgeList.Clear();
            userDetailsView.IsVisible = false;
            try
            {
                Loodinglayout.IsVisible = true;
                ulong uID;
                try
                {
                    uID = ulong.Parse(entryUserID.Text);
                }
                catch
                {
                    _ = DisplayAlert("Warning!", "unknown id.", "Ok");
                    Loodinglayout.IsVisible = false;
                    entryUserID.Text = "";
                    return;
                }

                entryUserID.Text = "";

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

                foreach (var item in user.PublicFlags.ToString().Split(new char[] { '|', ',' }).ToList())
                {
                    badgeList.Add(new badgeItems { imgSource = item.Trim() });
                }

                //imgAvatar.Source = user.GetAvatarUrl().Split('?')[0] + "?size=256";


                imgAvatar.Source = $"https://cdn.discordapp.com/avatars/{uID}/{avatar}?size=256";
                imgBanner.Source = $"https://cdn.discordapp.com/banners/{uID}/{banner}?size=512";

                lblUserName.Text = $"{username}#{discriminator}";
                imgIsBot.IsVisible = user.IsBot || user.IsWebhook;
                lblUserID.Text = user.Id.ToString();
                lblBannerColor.Text = banner_color;
                lblBannerColor.BackgroundColor = Xamarin.Forms.Color.FromHex(banner_color);
                lblCreationDate.Text = user.CreatedAt.ToString().Split('+')[0] + " UTC";

                userDetailsView.IsVisible = true;

                while (true)
                {
                    Debug.WriteLine($"Loading images: {imgAvatar.IsLoading} {imgBanner.IsLoading}");
                    if (!(imgAvatar.IsLoading || imgBanner.IsLoading))
                    {
                        Loodinglayout.IsVisible = false;
                        break;
                    }
                    else await Task.Delay(300);
                }
            }
            catch (Exception ex)
            {
                userDetailsView.IsVisible = false;
                Loodinglayout.IsVisible = false;
                _ = DisplayAlert("Error!", $"{ex.Message}", "Ok");
            }
        }

        private void testTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
        private async void DiscordButton_Clicked(object sender, EventArgs e)
        {
            await Browser.OpenAsync("https://discord.gg/aX4unxzZek");
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
    public class badgeItems
    {
        public string imgSource { get; set; }
    }
}