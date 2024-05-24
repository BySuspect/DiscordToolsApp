using CommunityToolkit.Maui.Views;

namespace DiscordToolsApp.Components.Popups.Common;

public partial class ImageViewPopup : Popup
{
    private string imgSource;

    public string ImgSource
    {
        get { return imgSource; }
        set
        {
            imgSource = value;
            OnPropertyChanged(nameof(ImgSource));
        }
    }

    public ImageViewPopup(string source)
    {
        InitializeComponent();
        BindingContext = this;

        ImgSource = source;
    }

    private async void Copy_Clicked(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(ImgSource);
        await ApplicationService.ShowShortToastAsync("Copied to clipboard!");
    }

    private void Close_Clicked(object sender, EventArgs e)
    {
        Close();
    }
}
