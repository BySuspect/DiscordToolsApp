using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DiscordToolsApp.Components.Popups.Common;

namespace DiscordToolsApp.Components.Partials.CustomItems
{
    public class CustomImage : Image
    {
        public static readonly BindableProperty IsImageLoadedProperty = BindableProperty.Create(
            nameof(IsImageLoaded),
            typeof(bool),
            typeof(CustomImage),
            false
        );

        public bool IsImageLoaded
        {
            get { return (bool)GetValue(IsImageLoadedProperty); }
            set { SetValue(IsImageLoadedProperty, value); }
        }

        public CustomImage()
        {
            this.Loaded += (object? sender, EventArgs e) =>
            {
                IsImageLoaded = true;
            };

            this.Unloaded += (object? sender, EventArgs e) =>
            {
                IsImageLoaded = false;
            };
        }
    }
}
