using DiscordToolsApp.Components.Partials.Events;

namespace DiscordToolsApp.Components.Partials.Views.TimestampPage;

public partial class DateView : ContentView
{
    public DateTime? SelectedDateTime
    {
        get { return CombineDateAndTime(dPicker.Date, tPicker.Time); }
        set { }
    }

    public DateView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public static DateTime CombineDateAndTime(DateTime? date, TimeSpan? time)
    {
        date ??= DateTime.Now.Date;
        time ??= DateTime.Now.TimeOfDay;

        return new DateTime(
            date.Value.Year,
            date.Value.Month,
            date.Value.Day,
            time.Value.Hours,
            time.Value.Minutes,
            0
        );
    }
}
