using DiscordToolsApp.Components.Partials.Events;

namespace DiscordToolsApp.Components.Partials.Views.TimestampPage;

public partial class DateView : ContentView
{
    private DateTime selectedDateTime;

    public DateTime SelectedDateTime
    {
        get { return selectedDateTime; }
        set
        {
            selectedDateTime = value;
            OnPropertyChanged(nameof(SelectedDateTime));
        }
    }

    public DateView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public event EventHandler<DateAndTimeChangedEventArgs> DateTimeChanged;

    protected virtual void OnDateTimeChanged(DateAndTimeChangedEventArgs e)
    {
        EventHandler<DateAndTimeChangedEventArgs> handler = DateTimeChanged;
        handler?.Invoke(this, e);
    }
}
