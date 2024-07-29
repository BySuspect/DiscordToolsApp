using CommunityToolkit.Maui.Views;

namespace DiscordToolsApp.Components.Popups.Common
{
    public partial class NewVersionNotifyPopup : Popup
    {
        public NewVersionNotifyPopup(string newVersion)
        {
            InitializeComponent();
            lblContent.Text = $"New version {newVersion} is available!\nDon't forget to update.";
        }

        private void btnClose_Clicked(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOpen_Clicked(object sender, EventArgs e)
        {
            var link = "https://github.com/BySuspect/DiscordToolsApp/releases/latest";
            Launcher.OpenAsync(link);
        }
    }
}
