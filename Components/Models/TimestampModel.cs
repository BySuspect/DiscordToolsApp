using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordToolsApp.Components.Models
{
    public class TimestampModel
    {
        public TimestampModel(int weeks, int days, int hours, int minutes)
        {
            Weeks = weeks;
            Days = days;
            Hours = hours;
            Minutes = minutes;
        }

        public int Weeks { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
    }
}
