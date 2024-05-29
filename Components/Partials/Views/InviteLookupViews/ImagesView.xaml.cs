using DiscordToolsApp.Components.Popups.Common;

namespace DiscordToolsApp.Components.Partials.Views.InviteLookupViews;

public partial class ImagesView : ContentView
{
    #region IconSource Binding
    public static readonly BindableProperty IconSourceProperty = BindableProperty.Create(
        nameof(IconSource),
        typeof(string),
        typeof(ImagesView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (ImagesView)bindable;
            if (string.IsNullOrWhiteSpace((string)newValue))
                control.iconView.IsVisible = false;
            else
                control.iconView.IsVisible = true;
        }
    );
    public string IconSource
    {
        get { return (string)GetValue(IconSourceProperty); }
        set
        {
            SetValue(IconSourceProperty, value);
            OnPropertyChanged(nameof(IconSource));
        }
    }
    #endregion

    #region BannerSource Binding
    public static readonly BindableProperty BannerSourceProperty = BindableProperty.Create(
        nameof(BannerSource),
        typeof(string),
        typeof(ImagesView),
        defaultValue: string.Empty,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (ImagesView)bindable;
            if (string.IsNullOrWhiteSpace((string)newValue))
                control.bannerView.IsVisible = false;
            else
                control.bannerView.IsVisible = true;
        }
    );
    public string BannerSource
    {
        get { return (string)GetValue(BannerSourceProperty); }
        set
        {
            SetValue(BannerSourceProperty, value);
            OnPropertyChanged(nameof(BannerSource));
        }
    }
    #endregion

    #region SplashSource Binding
    public static readonly BindableProperty SplashSourceProperty = BindableProperty.Create(
        nameof(SplashSource),
        typeof(string),
        typeof(ImagesView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (ImagesView)bindable;
            if (string.IsNullOrWhiteSpace((string)newValue))
                control.splashView.IsVisible = false;
            else
                control.splashView.IsVisible = true;
        }
    );
    public string SplashSource
    {
        get { return (string)GetValue(SplashSourceProperty); }
        set
        {
            SetValue(SplashSourceProperty, value);
            OnPropertyChanged(nameof(SplashSource));
        }
    }
    #endregion
    public ImagesView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void Icon_Tapped(object sender, TappedEventArgs e)
    {
        ApplicationService.ShowPopup(new ImageViewPopup(IconSource.Split('?')[0] + "?size=1024"));
    }

    private void Banner_Tapped(object sender, TappedEventArgs e)
    {
        ApplicationService.ShowPopup(new ImageViewPopup(BannerSource.Split('?')[0] + "?size=1024"));
    }

    private void Splash_Tapped(object sender, TappedEventArgs e)
    {
        ApplicationService.ShowPopup(new ImageViewPopup(SplashSource.Split('?')[0] + "?size=1024"));
    }
}
