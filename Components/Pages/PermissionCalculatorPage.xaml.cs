
using DiscordToolsApp.Components.Partials.Views.PermissionCalculatorViews;
using DiscordToolsApp.Components.Popups.PermissionCalculator;

using Microsoft.Maui.Controls.Compatibility;

namespace DiscordToolsApp.Components.Pages
{
    public partial class PermissionCalculatorPage : ContentPage
    {
        public PermissionCalculatorPage()
        {
            InitializeComponent();
        }


        private async void btnClear_Clicked(object sender, EventArgs e)
        {
            btnClear.IsEnabled = false;
            entryPermsInt.Text = "0";
            await PermsView.ClearPermissions();
            btnClear.IsEnabled = true;
        }

        private async void btnCopy_Clicked(object sender, EventArgs e)
        {
            btnCopy.IsEnabled = false;
            await Clipboard.SetTextAsync(entryPermsInt.Text);
            await ApplicationService.ShowShortToastAsync("Copied to clipboard");
            btnCopy.IsEnabled = true;
        }

        private void entryPermsInt_TextChanged(object sender, TextChangedEventArgs e)
        {
            ulong.TryParse(entryPermsInt.Text, out ulong permission);
            List<string> permissionList = DiscordPermissionHelper.ConvertPermissionIdsToTitles(DiscordPermissionHelper.ConvertIntegerToPermissionStrings(permission));
            lblOut.Text = $"{string.Join(", ", permissionList)}";
        }

        private void PermsView_PermissionsChanged(object sender, PermissionsChangedEventArgs e)
        {
            entryPermsInt.Text = DiscordPermissionHelper.ConvertPermissionStringsToInteger(e.NewValue).ToString();
        }

        private void entryPermsInt_TextComplated(object sender, EventArgs e)
        {
            ulong.TryParse(entryPermsInt.Text, out ulong permission);
            PermsView.SetPermissions(DiscordPermissionHelper.ConvertIntegerToPermissionStrings(permission));
        }
    }
}
