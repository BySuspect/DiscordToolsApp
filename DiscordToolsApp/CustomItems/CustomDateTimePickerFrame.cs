using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace DiscordToolsApp.CustomItems
{
    public class CustomDateTimePickerFrame : Frame
    {
        public CustomDateTimePickerFrame() : base()
        {
            //All Years
            List<string> _listofAllYears = new List<string>();
            for (int i = DateTime.MinValue.Year; i <= DateTime.MaxValue.Year; i++)
            {
                _listofAllYears.Add(i.ToString());
            }
            YearList = _listofAllYears;

            //All Months
            MonthList = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };

            //DaysInMonth
            List<string> _listofDaysOnMonth = new List<string>();
            for (int i = 1; i <= DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month); i++)
            {
                _listofDaysOnMonth.Add(i.ToString());
            }
            DayList = _listofDaysOnMonth;

            //Hours
            List<string> _listofhorus = new List<string>();
            for (int i = 0; i < 24; i++)
            {
                _listofhorus.Add(i.ToString("00"));
            }
            HourList = _listofhorus;

            //Minutes
            List<string> _listofminutes = new List<string>();
            for (int i = 0; i <= 59; i++)
            {
                _listofminutes.Add(i.ToString("00"));
            }
            MinuteList = _listofminutes;
        }

        #region Date
        string _selectedYear;
        public string SelectedYear
        {
            get { return _selectedYear ?? DateTime.UtcNow.Year.ToString(); }
            set { if (value != _selectedYear) { _selectedYear = value; OnPropertyChanged(nameof(SelectedYear)); } }
        }
        List<string> _yearList;
        public List<string> YearList
        {
            get { return _yearList; }
            set { if (value != _yearList) { _yearList = value; OnPropertyChanged(nameof(YearList)); } }
        }

        string _selectedMonth;
        public string SelectedMonth
        {
            get { return _selectedMonth ?? DateTime.UtcNow.Month.ToString(); }
            set { if (value != _selectedMonth) { _selectedMonth = value; OnPropertyChanged(nameof(SelectedMonth)); } }
        }
        List<string> _monthList;
        public List<string> MonthList
        {
            get { return _monthList; }
            set { if (value != _monthList) { _monthList = value; OnPropertyChanged(nameof(MonthList)); } }
        }

        string _selectedDay;
        public string SelectedDay
        {
            get { return _selectedDay ?? DateTime.UtcNow.Day.ToString(); }
            set { if (value != _selectedDay) { _selectedDay = value; OnPropertyChanged(nameof(SelectedDay)); } }
        }
        List<string> _dayList;
        public List<string> DayList
        {
            get { return _dayList; }
            set { if (value != _dayList) { _dayList = value; OnPropertyChanged(nameof(DayList)); } }
        }
        #endregion

        #region time
        string _selectedHour;
        public string SelectedHour
        {
            get { return _selectedHour ?? DateTime.UtcNow.Hour.ToString("00"); }
            set { if (value != _selectedHour) { _selectedHour = value; OnPropertyChanged(nameof(SelectedHour)); } }
        }
        List<string> _hourList;
        public List<string> HourList
        {
            get { return _hourList; }
            set { if (value != _hourList) { _hourList = value; OnPropertyChanged(nameof(HourList)); } }
        }
        string _selectedMinute;
        public string SelectedMinute
        {
            get { return _selectedMinute ?? DateTime.UtcNow.Minute.ToString("00"); }
            set { if (value != _selectedMinute) { _selectedMinute = value; OnPropertyChanged(nameof(SelectedMinute)); } }
        }
        List<string> _minuteList;
        public List<string> MinuteList
        {
            get { return _minuteList; }
            set { if (value != _minuteList) { _minuteList = value; OnPropertyChanged(nameof(MinuteList)); } }
        }
        #endregion

        [Obsolete]
        public string LocalTimeZone
        {
            get
            {
                DateTime localTime = DateTime.Now;
                TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
                TimeSpan utcOffset = localTimeZone.GetUtcOffset(localTime);

                return (utcOffset.Hours.ToString().Contains("-")) ? utcOffset.Hours.ToString() : "+" + utcOffset.Hours.ToString();
            }
        }

    }
}
