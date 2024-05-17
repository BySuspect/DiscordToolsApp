using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordToolsApp.Components.Partials.Events
{
    public class DateAndTimeChangedEventArgs : EventArgs
    {
        public DateAndTimeChangedEventArgs(DateTime? oldValue, DateTime? newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public DateTime? OldValue { get; private set; }
        public DateTime? NewValue { get; private set; }
    }
}
