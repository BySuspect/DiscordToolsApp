using DiscordToolsApp.Components.Partials.Events;

namespace DiscordToolsApp.Components.Partials.Views.TimestampPage;

public partial class DateView : ContentView
{
    private DateTime selectedDate;

    public DateTime SelectedDate
    {
        get { return selectedDate; }
        set
        {
            selectedDate = value;
            OnPropertyChanged(nameof(SelectedDate));
        }
    }

    private TimeSpan selectedTime;

    public TimeSpan SelectedTime
    {
        get { return selectedTime; }
        set
        {
            selectedTime = value;
            OnPropertyChanged(nameof(SelectedDate));
        }
    }

    public DateView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void datepicker_PropertyChanged(
        object sender,
        System.ComponentModel.PropertyChangedEventArgs e
    )
    {
        if (e.PropertyName == "Date")
        {
            var control = (DatePicker)sender;
            OnDateTimeChanged(
                new DateAndTimeChangedEventArgs(
                    this.SelectedDate,
                    this.SelectedTime,
                    control.Date,
                    this.SelectedTime
                )
            );
        }
    }

    private void timepicker_PropertyChanged(
        object sender,
        System.ComponentModel.PropertyChangedEventArgs e
    )
    {
        if (e.PropertyName == "Time")
        {
            var control = (TimePicker)sender;
            OnDateTimeChanged(
                new DateAndTimeChangedEventArgs(
                    this.SelectedDate,
                    this.SelectedTime,
                    this.SelectedDate,
                    control.Time
                )
            );
        }
    }

    public event EventHandler<DateAndTimeChangedEventArgs> DateTimeChanged;

    protected virtual void OnDateTimeChanged(DateAndTimeChangedEventArgs e)
    {
        EventHandler<DateAndTimeChangedEventArgs> handler = DateTimeChanged;
        handler?.Invoke(this, e);
    }
}
