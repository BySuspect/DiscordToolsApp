﻿using MarcTron.Plugin.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DiscordToolsApp.CustomItems
{
    public class CustomAdsView : MTAdView
    {
        private bool _isLoaded;

        public bool IsLoaded
        {
            get
            {
                return _isLoaded;
            }
            set
            {
                if (_isLoaded != value)
                {
                    _isLoaded = value;
                    OnPropertyChanged(nameof(IsLoaded));
                }
            }
        }

        public CustomAdsView() : base()
        {
            this.IsVisible = IsLoaded;
            this.AdsFailedToLoad += (s, e) =>
            {
                IsVisible = false;
                IsLoaded = false;
            };
            this.AdsLoaded += (s, e) =>
            {
                IsVisible = true;
                IsLoaded = true;
            };
        }
    }
}
