using DiscordToolsApp.Components.Partials.InputBehaviors;

namespace DiscordToolsApp.Components.Partials.Views.Shared;

public partial class NameAndValueTextView : ContentView
{
    #region Title Binding
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(NameAndValueTextView),
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
        typeof(string),
        typeof(NameAndValueTextView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (NameAndValueTextView)bindable;

            if (string.IsNullOrWhiteSpace((string)newValue))
                control.IsVisible = false;
            else
                control.IsVisible = true;

            if (UrlValidatorBehaviour.regexMatchs((string)newValue))
            {
                control.lblValue.TextDecorations = TextDecorations.Underline;
                control.lblValue.TextColor = Color.Parse("#4F99EF");
            }
            else
            {
                control.lblValue.TextDecorations = TextDecorations.None;
                control.lblValue.TextColor = AppThemeColors.TextColor;
            }

            if (ColorHexValidatorBehaviour.regexMatchs((string)newValue))
            {
                control.lblValue.BackgroundColor = Color.Parse((string)newValue);
            }
            else
            {
                control.lblValue.BackgroundColor = Colors.Transparent;
            }
        }
    );
    public string Value
    {
        get { return (string)GetValue(ValueProperty); }
        set
        {
            SetValue(ValueProperty, value);
            OnPropertyChanged(nameof(Value));
        }
    }
    #endregion
    public NameAndValueTextView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async void CopyValue_Tapped(object sender, TappedEventArgs e)
    {
        await Clipboard.SetTextAsync(Value);
        await ApplicationService.ShowShortToastAsync(Title.Replace(":", "") + " Copied!");
    }
}
