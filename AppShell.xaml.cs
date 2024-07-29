using DiscordToolsApp.Components.Pages;
using DiscordToolsApp.Components.Popups.Common;

namespace DiscordToolsApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("TimeStampGeneratorPage", typeof(TimeStampGeneratorPage));
            Routing.RegisterRoute("UserLookupPage", typeof(UserLookupPage));
            Routing.RegisterRoute("InviteLookupPage", typeof(InviteLookupPage));
            Routing.RegisterRoute("TextToEmojiPage", typeof(TextToEmojiPage));
            Routing.RegisterRoute("WebhookSendPage", typeof(WebhookSendPage));
            Routing.RegisterRoute("PermissionCalculatorPage", typeof(PermissionCalculatorPage));
#if DEBUG
            Routing.RegisterRoute("TestPage", typeof(TestPage));
            //this.GoToAsync("WebhookSendPage");
#endif
        }

        protected override bool OnBackButtonPressed()
        {
            if (Shell.Current.CurrentPage is not MainPage)
            {
                ApplicationService.ActivePage = "MainPage";
                Shell.Current.Navigation.PopAsync(true);
                return true;
            }

            return true;
        }

        private void Support_Clicked(object sender, EventArgs e)
        {
            string supportLink = "https://apps.shiroko.dev/supportus/";

            Launcher.OpenAsync(supportLink);
        }

        private void Discord_Clicked(object sender, EventArgs e)
        {
            string discorInvite = "https://discord.gg/aX4unxzZek";

            Browser.OpenAsync(discorInvite, BrowserLaunchMode.SystemPreferred);
        }
    }
}
