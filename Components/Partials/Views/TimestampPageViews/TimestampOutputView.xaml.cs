namespace DiscordToolsApp.Components.Partials.Views.TimestampPageViews;

public partial class TimestampOutputView : ContentView
{
    #region MainText Binding
    public static readonly BindableProperty MainTextProperty = BindableProperty.Create(
        nameof(MainText),
        typeof(string),
        typeof(TimestampOutputView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay
    );
    public string MainText
    {
        get { return (string)GetValue(MainTextProperty); }
        set
        {
            SetValue(MainTextProperty, value);
            OnPropertyChanged(nameof(MainText));
        }
    }
    #endregion

    #region PreviewText Binding
    public static readonly BindableProperty PreviewTextProperty = BindableProperty.Create(
        nameof(PreviewText),
        typeof(string),
        typeof(TimestampOutputView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay
    );
    public string PreviewText
    {
        get { return (string)GetValue(PreviewTextProperty); }
        set
        {
            SetValue(PreviewTextProperty, value);
            OnPropertyChanged(nameof(PreviewText));
        }
    }
    #endregion

    public TimestampOutputView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async void Copy_Clicked(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(MainText);
        await ApplicationService.ShowShortToastAsync(PreviewText + " Copied to clipboard");
    }
}
