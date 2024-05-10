using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordToolsApp.Components.Partials.Events
{
    public class NumberChangedEventArgs : EventArgs
    {
        public NumberChangedEventArgs(int oldValue, int newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public int OldValue { get; private set; }
        public int NewValue { get; private set; }
    }
}
