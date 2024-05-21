using DiscordToolsApp.Components.Models;

namespace DiscordToolsApp.Components.Partials.Views.MainPageViews;

public partial class MainPagCustomButtonView : ContentView
{
    #region PageType Binding
    public static readonly BindableProperty PageTypeProperty = BindableProperty.Create(
        nameof(PageType),
        typeof(MainPageButtonsPageTypeModel),
        typeof(MainPagCustomButtonView),
        defaultValue: MainPageButtonsPageTypeModel.MainPage,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (MainPagCustomButtonView)bindable;
            if (newValue is MainPageButtonsPageTypeModel.Empty)
                control.IsEnabled = false;
            else
                control.IsEnabled = true;
        }
    );
    public MainPageButtonsPageTypeModel PageType
    {
        get { return (MainPageButtonsPageTypeModel)GetValue(PageTypeProperty); }
        set
        {
            SetValue(PageTypeProperty, value);
            OnPropertyChanged(nameof(PageType));
        }
    }
    #endregion

    #region Text Binding
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(MainPagCustomButtonView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay
    );
    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set
        {
            SetValue(TextProperty, value);
            OnPropertyChanged(nameof(Text));
        }
    }
    #endregion

    #region Image Binding
    public static readonly BindableProperty ImageProperty = BindableProperty.Create(
        nameof(Image),
        typeof(string),
        typeof(MainPagCustomButtonView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay
    );
    public string Image
    {
        get { return (string)GetValue(ImageProperty); }
        set
        {
            SetValue(ImageProperty, value);
            OnPropertyChanged(nameof(Image));
        }
    }
    #endregion

    public MainPagCustomButtonView()
    {
        InitializeComponent();
        BindingContext = this;

        if (PageType is MainPageButtonsPageTypeModel.Empty)
            this.IsEnabled = false;
    }

    private void Frame_Tapped(object sender, TappedEventArgs e)
    {
        OnClicked(null);
    }

    public event EventHandler<ClickedEventArgs>? Clicked;

    protected virtual void OnClicked(ClickedEventArgs? e)
    {
        EventHandler<ClickedEventArgs>? handler = Clicked;
        handler?.Invoke(this, e);
    }
}
