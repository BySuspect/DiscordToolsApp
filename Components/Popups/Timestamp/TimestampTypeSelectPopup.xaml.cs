using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using DiscordToolsApp.Components.Partials.CustomItems;

namespace DiscordToolsApp.Components.Popups.Timestamp;

public partial class TimestampTypeSelectPopup : Popup
{
    public TimestampTypeSelectPopup()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async void img_Tapped(object sender, TappedEventArgs e)
    {
        var control = (CustomImageView)sender;
        await this.CloseAsync((string)control.Source);
    }
}
