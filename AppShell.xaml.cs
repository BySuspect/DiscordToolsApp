using DiscordToolsApp.Components.Pages;

namespace DiscordToolsApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
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
