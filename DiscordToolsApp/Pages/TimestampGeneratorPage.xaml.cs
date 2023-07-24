using DiscordToolsApp.Helpers;
using DiscordToolsApp.Pages.Popups;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.DateTimePopups;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordToolsApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimestampGeneratorPage : ContentPage
    {
        /*
         *<t:---:R>  - Relative
         *<t:---:t>  - Time
         *<t:---:T>  - Long Time
         *<t:---:d>  - Date
         *<t:---:D>  - Long Date
         *<t:---:f>  - Date Time
         *<t:---:F>  - Date Weekday
         */
        string format = "relative", timeframe = "timer";
        DateTime? selectedDate;
        TimeSpan? selectedTime;
        public TimestampGeneratorPage()
        {
            InitializeComponent();
            BindingContext = this;
            pickerFormat.SelectedIndex = 0;
            startTimeUpdate();
        }
        private async void btnCopyTimestamp_Clicked(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(TimestampText);
            ToastController.ShowShortToast("Copied!");
        }
        private void pickerFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            format = pickerFormat.SelectedItem.ToString().Trim().Replace(" ", "").ToLower();
            imageFormat.Source = pickerFormat.SelectedItem.ToString().Trim().Replace(" ", "");
            TimestampText = DateToTimestamp(CombineDateAndTime(selectedDate, selectedTime)).ToString();
            //if (timeframe != "timer") 

            //old
            //timeFrameDateTimePicker_PropertyChanged(null, null);
        }

        //void pickerTimeFrame_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    switch (pickerTimeFrame.SelectedItem)
        //    {
        //        case "Timer":
        //            timerPickerLayout.IsVisible = true;
        //            timeFrameDateTimePicker.IsVisible = false;
        //            Weeks = 0;
        //            Days = 0;
        //            Hours = 0;
        //            Minutes = 0;
        //            break;
        //        case "Date":
        //            TimestampText = AddTimeToTimestamp(0, 0, 0, 0).ToString();
        //            timerPickerLayout.IsVisible = false;
        //            timeFrameDateTimePicker.IsVisible = true;
        //            Weeks = 0;
        //            Days = 0;
        //            Hours = 0;
        //            Minutes = 0;
        //            break;
        //    }
        //    timeframe = pickerTimeFrame.SelectedItem.ToString().ToLower();
        //}

        string _timestampText = $"<t:{AddTimeToTimestamp(0, 0, 0, 0)}:R>";
        public string TimestampText
        {
            get
            {
                return _timestampText;
            }
            set
            {
                switch (format)
                {
                    case "relative":
                        if (_timestampText != $"<t:{value}:R>")
                        {
                            _timestampText = $"<t:{value}:R>";
                            OnPropertyChanged(nameof(TimestampText));
                        }
                        break;
                    case "time":
                        if (_timestampText != $"<t:{value}:t>")
                        {
                            _timestampText = $"<t:{value}:t>";
                            OnPropertyChanged(nameof(TimestampText));
                        }
                        break;
                    case "longtime":
                        if (_timestampText != $"<t:{value}:T>")
                        {
                            _timestampText = $"<t:{value}:T>";
                            OnPropertyChanged(nameof(TimestampText));
                        }
                        break;
                    case "date":
                        if (_timestampText != $"<t:{value}:d>")
                        {
                            _timestampText = $"<t:{value}:d>";
                            OnPropertyChanged(nameof(TimestampText));
                        }
                        break;
                    case "longdate":
                        if (_timestampText != $"<t:{value}:D>")
                        {
                            _timestampText = $"<t:{value}:D>";
                            OnPropertyChanged(nameof(TimestampText));
                        }
                        break;
                    case "datetime":
                        if (_timestampText != $"<t:{value}:f>")
                        {
                            _timestampText = $"<t:{value}:f>";
                            OnPropertyChanged(nameof(TimestampText));
                        }
                        break;
                    case "dateweekday":
                        if (_timestampText != $"<t:{value}:F>")
                        {
                            _timestampText = $"<t:{value}:F>";
                            OnPropertyChanged(nameof(TimestampText));
                        }
                        break;
                }
            }
        }
        void startTimeUpdate()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    while (true)
                    {
                        if (timeframe == "timer")
                            TimestampText = AddTimeToTimestamp(Weeks, Days, Hours, Minutes).ToString();
                        //else
                        //    TimestampText = DateToTimestamp(CombineDateAndTime(selectedDate, selectedTime)).ToString();
                        await Task.Delay(100);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogMessage($"TimestampError - Message: {ex.Message} - AppVersion: {References.Version}", LogLevel.Error);
                    Debug.WriteLine($"Error: {ex.Message}");
                }
            });
        }

        #region Timer
        int _weeks = 0;
        public int Weeks
        {
            get
            {
                return _weeks;
            }
            set
            {
                if (_weeks != value)
                {
                    _weeks = value;
                    OnPropertyChanged(nameof(Weeks));
                }
            }
        }

        int _days = 0;
        public int Days
        {
            get
            {
                return _days;
            }
            set
            {
                if (_days != value)
                {
                    _days = value;
                    OnPropertyChanged(nameof(Days));
                }
            }
        }

        int _hours = 0;
        public int Hours
        {
            get
            {
                return _hours;
            }
            set
            {
                if (_hours != value)
                {
                    _hours = value;
                    OnPropertyChanged(nameof(Hours));
                }
            }
        }

        int _minutes = 0;
        public int Minutes
        {
            get
            {
                return _minutes;
            }
            set
            {
                if (_minutes != value)
                {
                    _minutes = value;
                    OnPropertyChanged(nameof(Minutes));
                }
            }
        }

        private void addWeeksTapped(object sender, EventArgs e) => Weeks++;
        private void extractWeeksTapped(object sender, EventArgs e) => Weeks--;
        private void addDaysTapped(object sender, EventArgs e) => Days++;
        private void extractDaysTapped(object sender, EventArgs e) => Days--;
        private void addHoursTapped(object sender, EventArgs e) => Hours++;
        private void extractHoursTapped(object sender, EventArgs e) => Hours--;
        private void addMinutesTapped(object sender, EventArgs e) => Minutes++;
        private void extractMinutesTapped(object sender, EventArgs e) => Minutes--;
        public static long AddTimeToTimestamp(int weeks, int days, int hours, int minutes)
        {
            // Unix epoch date 1970-01-01 00:00:00 UTC
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Sum the entered values by converting them to TimeSpan object
            TimeSpan timeSpan = new TimeSpan((weeks * 7) + days, hours, minutes, 0);

            // Adding the aggregated TimeSpan to the DateTime object, the new date and time value is obtained
            DateTime newDateTime = DateTime.Now.Add(timeSpan);

            // The new date and time value is returned in Unix epoch timestamp format, calculating the time to Unix epoch time
            return (long)(newDateTime.ToUniversalTime() - unixEpoch).TotalSeconds;
        }
        #endregion

        #region Date
        private async void DateButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                selectedDate = await DateTimePopups.PickDateAsync();
                DateTime combinedDate = CombineDateAndTime(selectedDate, selectedTime);
                TimestampText = DateToTimestamp(combinedDate).ToString();
                lblSelectedDate.Text = selectedDate.Value.ToString("dd.MM.yyyy");
            }
            catch { }
        }

        private async void TimeButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                selectedTime = await DateTimePopups.PickTimeAsync();
                TimestampText = DateToTimestamp(CombineDateAndTime(selectedDate, selectedTime)).ToString();
                lblSelectedTime.Text = new DateTime(selectedTime.Value.Ticks, DateTimeKind.Local).ToString("hh:mm tt");
            }
            catch { }
        }
        #endregion

        private void mainTabView_SelectionChanged(object sender, TabSelectionChangedEventArgs e)
        {
            if (e.NewPosition == 0)
                timeframe = "timer";
            else if (e.NewPosition == 1)
            {
                timeframe = "Date";
                lblSelectedDate.Text = DateTime.Now.ToString("dd.MM.yyyy");
                lblSelectedTime.Text = DateTime.Now.ToString("hh:mm tt");
                TimestampText = DateToTimestamp(CombineDateAndTime(selectedDate, selectedTime)).ToString();
            }
        }
        private void btnTest_Clicked(object sender, EventArgs e)
        {
            Weeks = 0;
            Days = 0;
            Hours = 0;
            Minutes = 0;
        }

        // old
        //private void timeFrameDateTimePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    var obj = timeFrameDateTimePicker;
        //    TimestampText = DateToTimestamp(new DateTime(Int16.Parse(obj.SelectedYear),
        //                                                 Int16.Parse(obj.SelectedMonth),
        //                                                 Int16.Parse(obj.SelectedDay),
        //                                                 Int16.Parse(obj.SelectedHour),
        //                                                 Int16.Parse(obj.SelectedMinute),
        //                                                 0, DateTimeKind.Utc)).ToString();
        //}

        #region Genel Props
        public static long DateToTimestamp(DateTime dateTime)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan timeSpan = dateTime.ToUniversalTime() - unixEpoch;
            return (long)Math.Floor(timeSpan.TotalSeconds);
        }
        public static DateTime CombineDateAndTime(DateTime? date, TimeSpan? time)
        {
            if (date == null)
                date = DateTime.UtcNow.Date;
            if (time == null)
            {
                time = DateTime.UtcNow.TimeOfDay;
                return new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, time.Value.Hours, time.Value.Minutes, 0, DateTimeKind.Utc);
            }
            else
            {
                return new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, time.Value.Hours - getTimeOfset(), time.Value.Minutes, 0, DateTimeKind.Utc);
            }
        }
        public static int getTimeOfset()
        {
            DateTime localTime = DateTime.Now;
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            TimeSpan utcOffset = localTimeZone.GetUtcOffset(localTime);
            return utcOffset.Hours;
        }
        private void DiscordButton_Clicked(object sender, EventArgs e)
        {
            References.discordClicked();
        }
        private void FeedbackButton_Clicked(object sender, EventArgs e)
        {
            References.FeedbackClicked();
        }
        #endregion
    }
}