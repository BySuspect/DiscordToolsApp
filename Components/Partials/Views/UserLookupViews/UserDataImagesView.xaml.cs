using DiscordToolsApp.Components.Popups.Common;

namespace DiscordToolsApp.Components.Partials.Views.UserLookupViews;

public partial class UserDataImagesView : ContentView
{
    #region Avatar Binding
    public static readonly BindableProperty AvatarProperty = BindableProperty.Create(
        nameof(Avatar),
        typeof(string),
        typeof(UserDataImagesView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var view = (UserDataImagesView)bindable;

            if (string.IsNullOrWhiteSpace((string?)newValue))
                view.Avatar = "discordlogo.png";
        }
    );
    public string Avatar
    {
        get { return (string)GetValue(AvatarProperty); }
        set
        {
            SetValue(AvatarProperty, value);
            OnPropertyChanged(nameof(Avatar));
        }
    }
    #endregion

    #region Banner Binding
    public static readonly BindableProperty BannerProperty = BindableProperty.Create(
        nameof(Banner),
        typeof(string),
        typeof(UserDataImagesView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var view = (UserDataImagesView)bindable;
            if (string.IsNullOrWhiteSpace((string?)newValue))
            {
                view.userDetailBannerView.IsVisible = false;
            }
            else
                view.userDetailBannerView.IsVisible = true;
        }
    );
    public string Banner
    {
        get { return (string)GetValue(BannerProperty); }
        set
        {
            SetValue(BannerProperty, value);
            OnPropertyChanged(nameof(Banner));
        }
    }
    #endregion

    public UserDataImagesView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void Avatar_Tapped(object sender, TappedEventArgs e)
    {
        ApplicationService.ShowPopup(new ImageViewPopup(Avatar.Split('?')[0] + "?size=1024"));
    }

    private void Banner_Tapped(object sender, TappedEventArgs e)
    {
        if (userDetailBannerView.IsVisible)
            ApplicationService.ShowPopup(new ImageViewPopup(Banner.Split('?')[0] + "?size=1024"));
    }
}
