using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordToolsApp.Components.Partials.Events
{
    public class PermissionsChangedEventArgs : EventArgs
    {
        public PermissionsChangedEventArgs(List<string> oldValue, List<string> newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public List<string> OldValue { get; private set; }
        public List<string> NewValue { get; private set; }
    }
}
