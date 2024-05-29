using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DiscordToolsApp.Components.Popups.Common;

namespace DiscordToolsApp.Components.Partials.CustomItems
{
    class CustomImage : Image
    {
        public static readonly BindableProperty IsLoadedProperty = BindableProperty.Create(
            nameof(IsLoaded),
            typeof(bool),
            typeof(CustomImage),
            false
        );

        public bool IsLoaded
        {
            get { return (bool)GetValue(IsLoadedProperty); }
            set { SetValue(IsLoadedProperty, value); }
        }

        public CustomImage()
        {
            this.Loaded += (object? sender, EventArgs e) =>
            {
                IsLoaded = true;
            };

            this.Unloaded += (object? sender, EventArgs e) =>
            {
                IsLoaded = false;
            };
        }
    }
}
