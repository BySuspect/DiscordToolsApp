using DiscordToolsApp.Components.Partials.Events;

namespace DiscordToolsApp.Components.Partials.Views.TimestampPage;

public partial class TimerUpDownView : ContentView
{
    #region Value Binding
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(
        nameof(Value),
        typeof(int),
        typeof(TimerUpDownView),
        defaultValue: 0,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (TimerUpDownView)bindable;
            control.OnColorChanged(new NumberChangedEventArgs((int)oldValue, (int)newValue));
            if ((int)newValue > 0)
                control.plusTxt.Text = "+";
            else
                control.plusTxt.Text = "";
        }
    );
    public int Value
    {
        get { return (int)GetValue(ValueProperty); }
        set
        {
            SetValue(ValueProperty, value);
            OnPropertyChanged(nameof(Value));
        }
    }
    #endregion

    #region Text Binding
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(TimerUpDownView),
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

    public TimerUpDownView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public event EventHandler<NumberChangedEventArgs> ValueChanged;

    protected virtual void OnColorChanged(NumberChangedEventArgs e)
    {
        EventHandler<NumberChangedEventArgs> handler = ValueChanged;
        handler?.Invoke(this, e);
    }

    private void Minus_Clicked(object sender, EventArgs e)
    {
        Value--;
    }

    private void Plus_Clicked(object sender, EventArgs e)
    {
        Value++;
    }
}
