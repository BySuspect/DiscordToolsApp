using DiscordToolsApp.Components.Models;
using DiscordToolsApp.Components.Partials.Events;

namespace DiscordToolsApp.Components.Partials.Views.TimestampPage;

public partial class TimerView : ContentView
{
    private int months = 0;
    private int weeks = 0;
    private int days = 0;
    private int hours = 0;
    private int minutes = 0;

    public TimerView()
    {
        InitializeComponent();
    }

    private void ValueChanged(object sender, NumberChangedEventArgs e)
    {
        var control = (TimerUpDownView)sender;
        switch (control.AutomationId)
        {
            case "Months":
                months = e.NewValue;
                break;

            case "Weeks":
                weeks = e.NewValue;
                break;

            case "Days":
                days = e.NewValue;
                break;

            case "Hours":
                hours = e.NewValue;
                break;
            case "Minutes":
                minutes = e.NewValue;
                break;
        }

        OnTimestampChanged(new TimestampModel(months, weeks, days, hours, minutes));
    }

    public event EventHandler<TimestampModel> TimestampChanged;

    protected virtual void OnTimestampChanged(TimestampModel e)
    {
        EventHandler<TimestampModel> handler = TimestampChanged;
        handler?.Invoke(this, e);
    }
}
