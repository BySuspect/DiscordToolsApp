﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordWebhookRemoteApp.Services;

namespace DiscordToolsApp.Components.Partials.CustomItems
{
    public class CustomImageView : Frame
    {
        #region Source Binding
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            nameof(Source),
            typeof(string),
            typeof(CustomImageView),
            default(string)
        );

        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        #endregion

        private string errorSource;
        public string ErrorSource
        {
            get { return errorSource; }
            set
            {
                errorSource = value;
                OnPropertyChanged(nameof(ErrorSource));
            }
        }

        public CustomImageView()
        {
            var image = new Image();
            image.Source = ImageService.isImage(Source) ? Source : ErrorSource;

            this.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(Source))
                {
                    image.Source = ImageService.isImage(Source) ? Source : ErrorSource;
                }
                if (e.PropertyName == nameof(this.WidthRequest))
                {
                    image.WidthRequest = this.WidthRequest;
                }
                if (e.PropertyName == nameof(this.HeightRequest))
                {
                    image.HeightRequest = this.HeightRequest;
                }
            };

            this.BackgroundColor = Colors.Transparent;
            this.Padding = 0;
            this.Content = image;
        }
    }
}
