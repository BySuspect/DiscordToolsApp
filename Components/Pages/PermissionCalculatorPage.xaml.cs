
using DiscordToolsApp.Components.Popups.PermissionCalculator;

namespace DiscordToolsApp.Components.Pages
{
    public partial class PermissionCalculatorPage : ContentPage
    {
        public PermissionCalculatorPage()
        {
            InitializeComponent();
        }


        private void btnClear_Clicked(object sender, EventArgs e)
        {
            btnClear.IsEnabled = false;
            entryPermsInt.Text = "0";
            btnClear.IsEnabled = true;
        }

        private async void btnCopy_Clicked(object sender, EventArgs e)
        {
            btnCopy.IsEnabled = false;
            await Clipboard.SetTextAsync(entryPermsInt.Text);
            btnCopy.IsEnabled = true;
        }

        private void entryPermsInt_TextChanged(object sender, TextChangedEventArgs e)
        {
            ulong.TryParse(entryPermsInt.Text, out ulong permission);
            List<string> permissionList = DiscordPermissionHelper.ConvertPermissionIdsToTitles(DiscordPermissionHelper.ConvertIntegerToPermissionStrings(permission));

            lblOut.Text = $"{string.Join(", ", permissionList)}";
        }

        private async void btnSelect_Clicked(object sender, EventArgs e)
        {
            btnSelect.IsEnabled = false;
            ulong.TryParse(entryPermsInt.Text, out ulong perNum);
            var perms = await ApplicationService.ShowPopupAsync(new PermissionSelectAndViewPopup(DiscordPermissionHelper.ConvertIntegerToPermissionStrings(perNum)));
            if (perms is null) perms = new List<string>();
            entryPermsInt.Text = DiscordPermissionHelper.ConvertPermissionStringsToInteger((List<string>)perms).ToString();
            btnSelect.IsEnabled = true;
        }
    }
}
