using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Xamarin.Essentials;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.ObjectModel;
using DiscordToolsApp.Helpers;
using System.Threading;
using DiscordToolsApp.Pages.Popups;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;

namespace DiscordToolsApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class getUserDetailsPage : ContentPage
    {
        private ObservableCollection<badgeItems> badgeList = new ObservableCollection<badgeItems>();
        private const string token = "MTA0NDU3ODAxMDcxMzU2NzI3Mg.G98N5e.Wz5mtT6sf4g5-noSeda0Cv2HxSpWIw5zaBXRVQ";
        //private DiscordSocketClient _client;
        public getUserDetailsPage()
        {
            InitializeComponent();
            BindingContext = this;
            BindableLayout.SetItemsSource(badgesView, badgeList);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
#if DEBUG
            await Clipboard.SetTextAsync("272665050672660501");
            //Clipboard.SetTextAsync("190916650143318016");
            //Clipboard.SetTextAsync("282859044593598464");
            //Clipboard.SetTextAsync("901826325940154388");
#endif
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Debug.WriteLine("disposed");
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
                App.Current.MainPage = new Xamarin.Forms.NavigationPage(new getUserDetailsPage())
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

                //_client = new DiscordSocketClient();
                //await _client.LoginAsync(TokenType.Bot, token);
                //await _client.StartAsync();
                //IUser user;
                //while (true)
                //{
                //    if (_client.ConnectionState == ConnectionState.Connected)
                //    {
                //        user = await _client.GetUserAsync(uID, RequestOptions.Default);
                //        Debug.WriteLine("Connected!");
                //        break;
                //    }
                //    await Task.Delay(100);

                //    Debug.WriteLine("Not Connected! " + _client.ConnectionState + _client.LoginState);
                //}
                //await _client.LogoutAsync();
                //_client.Dispose();

                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bot {token}");

                var response = await client.GetAsync($"https://discord.com/api/users/{uID}");

                var result = await response.Content.ReadAsStringAsync();
                var userjson = JsonConvert.DeserializeObject<JObject>(result);

                ulong id = (ulong)userjson["id"];
                string? username = (string)userjson["username"];
                string? global_name = (string)userjson["global_name"];
                string? display_name = (string)userjson["display_name"];
                string? avatar = (string)userjson["avatar"];
                string? avatar_decoration = (string)userjson["avatar_decoration"];
                string? discriminator = (string)userjson["discriminator"];
                int? public_flags = (int?)userjson["public_flags"];
                var isBot = userjson["bot"];
                int? flags = (int)userjson["flags"];
                string? banner = (string)userjson["banner"];
                string? banner_color = (string)userjson["banner_color"];
                string? accent_color = (string)userjson["accent_color"];


                foreach (var item in ConvertFlagsToList((int)public_flags))
                {
                    badgeList.Add(new badgeItems { imgSource = item.Trim() });
                }

                //imgAvatar.Source = user.GetAvatarUrl().Split('?')[0] + "?size=256";
                imgAvatar.Source = $"https://cdn.discordapp.com/avatars/{uID}/{avatar}?size=256";
                //imgAvatarDecor.Source = $"https://cdn.discordapp.com/avatar-decoration-presets/{avatar_decoration}";
                imgBanner.Source = $"https://cdn.discordapp.com/banners/{uID}/{banner}?size=512";
                lblUserName.Text = $"{username}#{discriminator}";
                imgIsBot.IsVisible = (bool)((isBot) ?? false);
                lblUserID.Text = id.ToString();
                lblBannerColor.Text = banner_color;
                lblBannerColor.BackgroundColor = Xamarin.Forms.Color.FromHex(banner_color ?? "#00FFFFFF");
                lblCreationDate.Text = GetTimestampFromSnowflake(id).ToString();

                userDetailsView.IsVisible = true;

                while (true)
                {
                    Debug.WriteLine($"Loading images: {imgAvatar.IsLoading} {imgBanner.IsLoading}");
                    if (!(imgAvatar.IsLoading || imgBanner.IsLoading || imgAvatarDecor.IsLoading))
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
        public static List<string> ConvertFlagsToList(int flags)
        {
            List<string> flagList = new List<string>();

            if ((flags & (1 << 0)) != 0)
                flagList.Add("STAFF");
            if ((flags & (1 << 1)) != 0)
                flagList.Add("PARTNER");
            if ((flags & (1 << 2)) != 0)
                flagList.Add("HYPESQUAD");
            if ((flags & (1 << 3)) != 0)
                flagList.Add("BUG_HUNTER_LEVEL_1");
            if ((flags & (1 << 6)) != 0)
                flagList.Add("HYPESQUAD_ONLINE_HOUSE_1");
            if ((flags & (1 << 7)) != 0)
                flagList.Add("HYPESQUAD_ONLINE_HOUSE_2");
            if ((flags & (1 << 8)) != 0)
                flagList.Add("HYPESQUAD_ONLINE_HOUSE_3");
            if ((flags & (1 << 9)) != 0)
                flagList.Add("PREMIUM_EARLY_SUPPORTER");
            if ((flags & (1 << 10)) != 0)
                flagList.Add("TEAM_PSEUDO_USER");
            if ((flags & (1 << 14)) != 0)
                flagList.Add("BUG_HUNTER_LEVEL_2");
            if ((flags & (1 << 16)) != 0)
                flagList.Add("VERIFIED_BOT");
            if ((flags & (1 << 17)) != 0)
                flagList.Add("VERIFIED_DEVELOPER");
            if ((flags & (1 << 18)) != 0)
                flagList.Add("CERTIFIED_MODERATOR");
            if ((flags & (1 << 19)) != 0)
                flagList.Add("BOT_HTTP_INTERACTIONS");
            if ((flags & (1 << 22)) != 0)
                flagList.Add("ACTIVE_DEVELOPER");

            return flagList;
        }
        public static DateTime GetTimestampFromSnowflake(ulong snowflake)
        {
            const long DiscordEpoch = 1420070400000L;
            const ulong TimestampMask = 0xFFFFFFFFFFC00000UL;
            const ulong WorkerIdMask = 0x3E0000UL;
            const ulong ProcessIdMask = 0x1F000UL;
            const ulong IncrementMask = 0xFFFUL;

            long timestamp = (long)((snowflake & TimestampMask) >> 22) + DiscordEpoch;
            int workerId = (int)((snowflake & WorkerIdMask) >> 17);
            int processId = (int)((snowflake & ProcessIdMask) >> 12);
            int increment = (int)(snowflake & IncrementMask);

            DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime;
            Console.WriteLine($"Timestamp: {dateTime}");
            Console.WriteLine($"Worker ID: {workerId}");
            Console.WriteLine($"Process ID: {processId}");
            Console.WriteLine($"Increment: {increment}");

            return dateTime;
        }
        private void testTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
        private async void Banner_Tapped(object sender, EventArgs e)
        {
            var uriImageSource = imgBanner.Source as UriImageSource;
            if (uriImageSource != null)
            {
                await Clipboard.SetTextAsync(uriImageSource.Uri.AbsoluteUri.ToString());
                ToastController.ShowShortToast("Banner Link Copied!");
            }
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
    public class badgeItems
    {
        public string imgSource { get; set; }
    }
}