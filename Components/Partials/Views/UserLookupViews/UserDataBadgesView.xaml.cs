namespace DiscordToolsApp.Components.Partials.Views.UserLookupViews;

public partial class UserDataBadgesView : ContentView
{
    #region Title Binding
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(UserDataBadgesView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay
    );
    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set
        {
            SetValue(TitleProperty, value);
            OnPropertyChanged(nameof(Title));
        }
    }
    #endregion

    #region Value Binding
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(
        nameof(Value),
        typeof(string[]),
        typeof(UserDataTextView),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay
    );
    public string[] Value
    {
        get { return (string[])GetValue(ValueProperty); }
        set
        {
            SetValue(ValueProperty, value);
            OnPropertyChanged(nameof(Value));
        }
    }
    #endregion
    public UserDataBadgesView()
    {
        InitializeComponent();
        BindingContext = this;
    }
}
