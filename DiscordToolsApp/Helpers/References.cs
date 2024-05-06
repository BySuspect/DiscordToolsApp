using DiscordToolsApp.Pages;
using DiscordToolsApp.Pages.Popups;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DiscordToolsApp.Helpers
{
    public class References
    {
        public static bool supportPopup = true;
        public static string Version = "1.0.4";
        public static async Task<bool> CheckConnection()
        {
            var client = new HttpClient();
            try
            {
                await client.GetAsync("https://www.google.com");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string CurrentVersion { get; private set; }
        public static string ServerVersion { get; private set; }
        public static async Task CheckAppVersion()
        {
            // Mevcut sürüm numarasını al
            CurrentVersion = Version; // Kendi mevcut sürüm numaranızı buraya yazın

            // Sunucudan sürüm numarasını al
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync("https://awgstudiosapps.web.app/discordtools/version.txt"); // Sürüm numarasını döndüren sunucu URL'sini buraya yazın
                    response.EnsureSuccessStatusCode();
                    ServerVersion = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda nasıl bir işlem yapmak istediğinizi burada belirtebilirsiniz
                Console.WriteLine("Sürüm numarası alınamadı: " + ex.Message);
                Logger.LogMessage($"App Version Error - Message: {ex.Message} - AppVersion: {CurrentVersion}", LogLevel.Error);
                return;
            }

            // Sürüm numaralarını karşılaştır
            if (ServerVersion != CurrentVersion)
            {
#if !DEBUG
                // Popup göster
                await App.Current.MainPage.DisplayAlert("Update Available", "A new version is available. Please update the app.", "Ok");
#endif
            }
        }


        #region Discord Invite Section
        public static string discorInviteShorten = "https://bit.ly/3NmBFDO";
        public static string discorInvite = "https://discord.gg/aX4unxzZek";
        public static async void discordClicked()
        {
            var res = await App.Current.MainPage.DisplayActionSheet("Discord Invite", "", "Cancel", "Open Link", "Copy Link");
            if (res == "Open Link")
            {
                await Launcher.OpenAsync(discorInviteShorten);
            }
            else if (res == "Copy Link")
            {
                await Clipboard.SetTextAsync(discorInvite);
                ToastController.ShowShortToast("Discord Link Copied!");
            }
        }
        #endregion

        #region Feedback Section
        public static async void FeedbackClicked()
        {
            try
            {
                int counter = 1;
                if (Preferences.Get("user_feedback_date", DateTime.Now.DayOfYear) == DateTime.Now.DayOfYear)
                {
                    counter = Preferences.Get("user_feedback_count", 1);
                }
                else
                {
                    Preferences.Set("user_feedback_date", DateTime.Now.DayOfYear);
                    Preferences.Set("user_feedback_count", 1);
                    counter = 1;
                }

                if (counter <= 5)
                {
                    Popup popup = new FeedbackPopupPage();
                    var res = await App.Current.MainPage.Navigation.ShowPopupAsync(popup);
                    if (res.ToString() == "counterror")
                    {
                        ToastController.ShowShortToast("You reached daily feedback limit.");
                    }
                    else if (res.ToString() == "catcherror")
                    {
                        await App.Current.MainPage.DisplayAlert("Error!", "Something went wrong try again later.", "Ok");
                    }
                }
                else
                {
                    ToastController.ShowShortToast("You reached daily feedback limit.");
                }

            }
            catch
            {

            }
        }
        #endregion
    }

    #region ThemeCodes
    public static class ChangeAppTheme
    {
        public static void LightTheme()
        {
            ThemeColors.TextColor = Color.Black;
            ThemeColors.TransparentTextColor = Color.FromHex("#BA000000");
            ThemeColors.BorderColor = Color.Black;
            ThemeColors.BorderBackColor = Color.Transparent;
            ThemeColors.BackColor = Color.FromHex("#DBDBDB");
            ThemeColors.StatusBarColor = Color.FromHex("#FFFFFF");
            ThemeColors.StatusBarStyle = StatusBarStyle.DarkContent;
            ThemeColors.backgroundImg = null;
        }
        public static void DarkTheme()
        {
            ThemeColors.TextColor = Color.White;
            ThemeColors.TransparentTextColor = Color.FromHex("#BAFFFFFF");
            ThemeColors.BorderColor = Color.FromHex("#FFFFFF");
            ThemeColors.BorderBackColor = Color.Transparent;
            ThemeColors.BackColor = Color.FromHex("#101010");
            ThemeColors.StatusBarColor = Color.FromHex("#000000");
            ThemeColors.StatusBarStyle = StatusBarStyle.LightContent;
            ThemeColors.backgroundImg = null;
        }
        public static void ForDenizTheme()
        {
            ThemeColors.TextColor = Color.FromHex("#000000");
            ThemeColors.TransparentTextColor = Color.FromHex("#BA470041");
            ThemeColors.BorderColor = Color.FromHex("#000000");
            ThemeColors.BorderBackColor = Color.FromHex("#55fc03d7");
            ThemeColors.BackColor = Color.FromHex("#fc03d7");
            ThemeColors.StatusBarColor = Color.FromHex("#fc03d7");
            ThemeColors.StatusBarStyle = StatusBarStyle.DarkContent;
            ThemeColors.backgroundImg = "wallpaperfordeniz.jpg";
        }
    }
    public static class ThemeColors
    {
        public static Color TextColor { get; set; }
        public static Color TransparentTextColor { get; set; }
        public static Color BorderColor { get; set; }
        public static Color BorderBackColor { get; set; }
        public static Color BackColor { get; set; }
        public static Color StatusBarColor { get; set; }
        public static StatusBarStyle StatusBarStyle { get; set; }
        public static ImageSource backgroundImg { get; set; }
    }
    public class ThemeChangedEventArgs : EventArgs
    {
        public ThemeChangedEventArgs(string newTheme)
        {
            NewTheme = newTheme;
        }
        public string NewTheme { get; private set; }
    }

    public delegate void ThemeChangedEventHandler(object sender, ThemeChangedEventArgs e);
    public static class Theme
    {
        public static event ThemeChangedEventHandler ThemeChanged;

        public static void OnThemeChanged(string newTheme)
        {
            ThemeChanged?.Invoke(null, new ThemeChangedEventArgs(newTheme));
        }
    }
    #endregion
}
