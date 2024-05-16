using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordToolsApp.Components.Partials.Events
{
    public class DateAndTimeChangedEventArgs : EventArgs
    {
        public DateAndTimeChangedEventArgs(
            DateTime? oldDate,
            TimeSpan? oldTime,
            DateTime? newDate,
            TimeSpan? newTime
        )
        {
            OldDate = oldDate;
            OldTime = oldTime;
            NewDate = newDate;
            NewTime = newTime;
        }

        public DateTime? OldDate { get; private set; }
        public TimeSpan? OldTime { get; private set; }
        public DateTime? NewDate { get; private set; }
        public TimeSpan? NewTime { get; private set; }
    }
}
