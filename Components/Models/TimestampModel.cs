using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordToolsApp.Components.Models
{
    public class TimestampModel
    {
        public TimestampModel(int weeks = 0, int days = 0, int hours = 0, int minutes = 0)
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
