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
        typeof(UserDataBadgesView),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (UserDataBadgesView)bindable;
            if (newValue is null)
                control.IsVisible = false;
            else if ((newValue as string[]).Length == 0)
                control.IsVisible = false;
            else
                control.IsVisible = true;
        }
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
