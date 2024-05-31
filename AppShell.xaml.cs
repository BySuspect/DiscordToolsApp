using DiscordToolsApp.Components.Pages;
using DiscordToolsApp.Components.Popups.Common;

namespace DiscordToolsApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            if (Preferences.Get("PrivacyPolicyV1Accepted", false))
                ApplicationService.ShowPopup(new PrivacyPolicyPopup());
        }

        protected override bool OnBackButtonPressed()
        {
            if (Shell.Current.CurrentPage is not MainPage)
            {
                ApplicationService.ActivePage = "MainPage";
                Shell.Current.GoToAsync("//MainPage", true);
                return true;
            }

            return true;
        }

        private void Discord_Clicked(object sender, EventArgs e)
        {
            string discorInvite = "https://discord.gg/aX4unxzZek";

            Browser.OpenAsync(discorInvite, BrowserLaunchMode.SystemPreferred);
        }
    }
}
