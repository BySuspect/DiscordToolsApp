using DiscordToolsApp.Components.Partials.CustomItems;
using Newtonsoft.Json;

namespace DiscordToolsApp.Components.Pages;

public partial class TimeStampGeneratorPage : ContentPage
{
    /*
     *<t:---:d>  - date
     *<t:---:f>  - date_time
     *<t:---:F>  - date_weekday
     *<t:---:D>  - long_date
     *<t:---:T>  - long_time
     *<t:---:R>  - relative
     *<t:---:t>  - time
     */

    private Models.TimestampModel timestamp = new Models.TimestampModel();
    private string mode = "timer";

    public TimeStampGeneratorPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        Task.Run(TimestampLoop);
    }

    [Obsolete]
    public Task TimestampLoop()
    {
        Device.BeginInvokeOnMainThread(async () =>
        {
            while (true)
            {
                if (ApplicationService.ActivePage != "TimeStampGeneratorPage")
                    break;

                long tmp;
                if (this.mode is "timer")
                {
                    tmp = GetTimestamp(
                        null,
                        timestamp.Months,
                        timestamp.Weeks,
                        timestamp.Days,
                        timestamp.Hours,
                        timestamp.Minutes
                    );
                }
                else
                    tmp = GetTimestamp(dateView.SelectedDateTime);

                tmpDate.MainText = $"<t:{tmp}:d>";
                tmpDate.PreviewText = GetDateStringFromTimestamp(tmp, "date");

                tmpDateTime.MainText = $"<t:{tmp}:f>";
                tmpDateTime.PreviewText = GetDateStringFromTimestamp(tmp, "date_time");

                tmpDateWeekday.MainText = $"<t:{tmp}:F>";
                tmpDateWeekday.PreviewText = GetDateStringFromTimestamp(tmp, "date_weekday");

                tmpLongDate.MainText = $"<t:{tmp}:D>";
                tmpLongDate.PreviewText = GetDateStringFromTimestamp(tmp, "long_date");

                tmpLongTime.MainText = $"<t:{tmp}:T>";
                tmpLongTime.PreviewText = GetDateStringFromTimestamp(tmp, "long_time");

                tmpRelative.MainText = $"<t:{tmp}:R>";
                tmpRelative.PreviewText = GetDateStringFromTimestamp(tmp, "relative");

                tmpTime.MainText = $"<t:{tmp}:t>";
                tmpTime.PreviewText = GetDateStringFromTimestamp(tmp, "time");

                await Task.Delay(100);
            }
        });
        return Task.CompletedTask;
    }

    private void TimestampChanged(object sender, Models.TimestampModel e) => timestamp = e;

    public long GetTimestamp(
        DateTime? date = null,
        int months = 0,
        int weeks = 0,
        int days = 0,
        int hours = 0,
        int minutes = 0
    )
    {
        DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan timeSpan = new TimeSpan((months * 30) + (weeks * 7) + days, hours, minutes, 0);
        DateTime newDateTime = (date ?? DateTime.Now).Add(timeSpan);
        return (long)(newDateTime.ToUniversalTime() - unixEpoch).TotalSeconds;
    }

    public DateTime GetDateTime(
        DateTime? date = null,
        int months = 0,
        int weeks = 0,
        int days = 0,
        int hours = 0,
        int minutes = 0
    )
    {
        if (months == 0)
            months = this.timestamp.Months;
        if (weeks == 0)
            weeks = this.timestamp.Weeks;
        if (days == 0)
            days = this.timestamp.Days;
        if (hours == 0)
            hours = this.timestamp.Hours;
        if (minutes == 0)
            minutes = this.timestamp.Minutes;

        TimeSpan timeSpan = new TimeSpan((months * 30) + (weeks * 7) + days, hours, minutes, 0);
        return (date ?? DateTime.Now).Add(timeSpan);
    }

    public DateTime GetDateTimeFromTimestamp(long timestamp)
    {
        DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return unixEpoch.AddSeconds(timestamp).ToLocalTime();
    }

    public string GetDateStringFromTimestamp(long timestamp, string type)
    {
        var dateTime = GetDateTimeFromTimestamp(timestamp);

        switch (type)
        {
            case "date":
                return dateTime.ToString("MM/dd/yyyy");
            case "date_time":
                return dateTime.ToString("MMM dd, yyyy hh:mm tt");
            case "date_weekday":
                return dateTime.ToString("dddd, MMM dd, yyyy hh:mm tt");
            case "long_date":
                return dateTime.ToString("MMM dd, yyyy");
            case "long_time":
                return dateTime.ToString("h:mm:ss tt");
            case "relative":
                return GetRelativeTimeStampString(dateTime);
            case "time":
                return dateTime.ToString("h:mm tt");
            default:
                return "ERROR";
        }
    }

    private string GetRelativeTimeStampString(DateTime? date = null)
    {
        TimeSpan dateDiff = (TimeSpan)(DateTime.Now - date);
        bool isFuture = dateDiff.TotalMilliseconds < 0;

        dateDiff = TimeSpan.FromSeconds(Math.Abs(dateDiff.TotalSeconds));

        if ((int)dateDiff.TotalSeconds == 0)
        {
            return "just now";
        }
        else if (RoundTime((decimal)dateDiff.TotalSeconds) < 60)
        {
            if (isFuture)
                return $"in {(int)dateDiff.TotalSeconds + 1} seconds";
            else
                return $"{(int)dateDiff.TotalSeconds} seconds ago";
        }
        else if (RoundTime((decimal)dateDiff.TotalSeconds) < 60 * 60)
        {
            if (isFuture)
                return $"in {(int)dateDiff.TotalSeconds / 60 + 1} minutes";
            else
                return $"{(int)dateDiff.TotalSeconds / 60} minutes ago";
        }
        else if (RoundTime((decimal)dateDiff.TotalSeconds) < 60 * 60 * 24)
        {
            if (isFuture)
                return $"in {(int)dateDiff.TotalSeconds / 60 / 60 + 1} hours";
            else
                return $"{(int)dateDiff.TotalSeconds / 60 / 60} hours ago";
        }
        else if (RoundTime((decimal)dateDiff.TotalSeconds) < 60 * 60 * 24 * 30)
        {
            if (isFuture)
                return $"in {(int)dateDiff.TotalSeconds / 60 / 60 / 24 + 1} days";
            else
                return $"{(int)dateDiff.TotalSeconds / 60 / 60 / 24} days ago";
        }
        else
        {
            int months = (int)(RoundTime((decimal)dateDiff.TotalSeconds) / 60 / 60 / 24 / 30);

            if (months == 0)
                months = 1;

            int years = months / 12;
            if (years > 0)
            {
                if (isFuture)
                    return $"in {years} years";
                else
                    return $"{years} years ago";
            }
            else
            {
                if (isFuture)
                    return $"in {months} months";
                else
                    return $"{months} months ago";
            }
        }
    }

    private decimal RoundTime(decimal value)
    {
        value = (long)value;

        if (value == 59)
            return 60;
        else if (value == (60 * 60) - 1)
            return 60 * 60;
        else if (value == (60 * 60 * 24) - 1)
            return 60 * 60 * 24;
        else if (value == (60 * 60 * 24 * 30) - 1)
            return 60 * 60 * 24 * 30;
        else
            return value + 1;
    }

    private void TabItemTapped(object sender, Syncfusion.Maui.TabView.TabItemTappedEventArgs e)
    {
        switch (e.TabItem.Header)
        {
            case "Timer":
                mode = "timer";
                break;

            case "Date & Time":
                mode = "datetime";
                break;
        }
    }
}
