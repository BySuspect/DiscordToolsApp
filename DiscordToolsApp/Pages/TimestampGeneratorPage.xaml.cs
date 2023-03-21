using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordToolsApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimestampGeneratorPage : ContentPage
    {
        public TimestampGeneratorPage()
        {
            InitializeComponent();
            BindingContext = this;
            startTimeUpdate();
        }
        void startTimeUpdate()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    while (true)
                    {
                        TimestampText = AddTimeToTimestamp(Weeks, Days, Hours, Minutes).ToString();
                        await Task.Delay(50);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata: {ex.Message}");
                }
            });
        }
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

        string _timestampText = AddTimeToTimestamp(0, 0, 0, 0).ToString();
        public string TimestampText
        {
            get
            {
                return _timestampText;
            }
            set
            {
                if (_timestampText != value)
                {
                    _timestampText = value;
                    OnPropertyChanged(nameof(TimestampText));
                }
            }
        }

        private void btnTest_Clicked(object sender, EventArgs e)
        {
            Weeks = 0;
            Days = 0;
            Hours = 0;
            Minutes = 0;
        }

        private void addWeeksTapped(object sender, EventArgs e)
        {
            Weeks++;
        }
        private void addDaysTapped(object sender, EventArgs e)
        {
            Days++;
        }
        private void addHoursTapped(object sender, EventArgs e)
        {
            Hours++;
        }
        private void addMinutesTapped(object sender, EventArgs e)
        {
            Minutes++;
        }
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
    }
}