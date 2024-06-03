using CommunityToolkit.Maui.Views;

namespace DiscordToolsApp.Components.Popups.Common;

public partial class ImageViewPopup : Popup
{
    public ImageViewPopup(string source)
    {
        InitializeComponent();
        BindingContext = this;
        imgView.Source = source;
    }

    private async void Copy_Clicked(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(imgView.Source);
        await ApplicationService.ShowShortToastAsync("Copied to clipboard!");
    }

    private void Close_Clicked(object sender, EventArgs e)
    {
        Close();
    }
}
