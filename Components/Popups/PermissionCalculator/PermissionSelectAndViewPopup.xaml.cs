using CommunityToolkit.Maui.Views;

namespace DiscordToolsApp.Components.Popups.PermissionCalculator
{
    public partial class PermissionSelectAndViewPopup : Popup
    {
        public PermissionSelectAndViewPopup(List<string>? permsList = null)
        {
            InitializeComponent();

            if (permsList is not null && permsList.Count > 0)
            {
                cbPermsView.SetPermissions(permsList);
            }
        }

        private void bynCancel_Clicked(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Clicked(object sender, EventArgs e)
        {
            Close(cbPermsView.PermissionsList);
        }
    }
}
