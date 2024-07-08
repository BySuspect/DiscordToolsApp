
using CommunityToolkit.Maui.Core.Extensions;

using DiscordToolsApp.Components.Models;

namespace DiscordToolsApp.Components.Pages
{
    public partial class PermissionCalculatorPage : ContentPage
    {
        public PermissionCalculatorPage()
        {
            InitializeComponent();
        }

        private void btnConvert_Clicked(object sender, EventArgs e)
        {
            ulong permission = ulong.Parse(entryPermissionInteger.Text);
            List<string> permissionList = DiscordPermissionHelper.ConvertIntegerToPermissionStrings(permission);
            ulong perNum = DiscordPermissionHelper.ConvertPermissionStringsToInteger(cbPermsView.PermissionsList);

            lblOut.Text = $"{string.Join(", ", permissionList)}\n--\n{perNum}";
        }

        private void cbPermsView_PermissionsChanged(object sender, PermissionsChangedEventArgs e)
        {
            ulong permission = ulong.Parse(entryPermissionInteger.Text);
            List<string> permissionList = DiscordPermissionHelper.ConvertIntegerToPermissionStrings(permission);
            ulong perNum = DiscordPermissionHelper.ConvertPermissionStringsToInteger(cbPermsView.PermissionsList);

            lblOut.Text = $"{string.Join(", ", permissionList)}\n--\n{perNum}";
        }
    }
}
