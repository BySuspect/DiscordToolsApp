using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.Forms;

namespace DiscordToolsApp.Helpers
{
    public class References
    {
        public static bool supportPopup = true;
    }
    public static class ChangeAppTheme
    {
        public static void LightTheme()
        {
            ThemeColors.TextColor = Color.Black;
            ThemeColors.TransparentTextColor = Color.FromHex("#BA000000");
            ThemeColors.BorderColor = Color.Black;
            ThemeColors.BorderBackColor = Color.Transparent;
            ThemeColors.BackColor = Color.FromHex("#DBDBDB");
            ThemeColors.StatusBarColor = Color.FromHex("#FFFFFF");
            ThemeColors.StatusBarStyle = StatusBarStyle.DarkContent;
            ThemeColors.backgroundImg = null;
        }
        public static void DarkTheme()
        {
            ThemeColors.TextColor = Color.White;
            ThemeColors.TransparentTextColor = Color.FromHex("#BAFFFFFF");
            ThemeColors.BorderColor = Color.White;
            ThemeColors.BorderBackColor = Color.Transparent;
            ThemeColors.BackColor = Color.FromHex("#101010");
            ThemeColors.StatusBarColor = Color.FromHex("#000000");
            ThemeColors.StatusBarStyle = StatusBarStyle.LightContent;
            ThemeColors.backgroundImg = null;
        }
        public static void ForDenizTheme()
        {
            ThemeColors.TextColor = Color.FromHex("#000000");
            ThemeColors.TransparentTextColor = Color.FromHex("#BA470041");
            ThemeColors.BorderColor = Color.FromHex("#000000");
            ThemeColors.BorderBackColor = Color.FromHex("#55fc03d7");
            ThemeColors.BackColor = Color.FromHex("#fc03d7");
            ThemeColors.StatusBarColor = Color.FromHex("#fc03d7");
            ThemeColors.StatusBarStyle = StatusBarStyle.DarkContent;
            ThemeColors.backgroundImg = "wallpaperfordeniz.jpg";
        }
    }
    public static class ThemeColors
    {
        public static Color TextColor { get; set; }
        public static Color TransparentTextColor { get; set; }
        public static Color BorderColor { get; set; }
        public static Color BorderBackColor { get; set; }
        public static Color BackColor { get; set; }
        public static Color StatusBarColor { get; set; }
        public static StatusBarStyle StatusBarStyle { get; set; }
        public static ImageSource backgroundImg { get; set; }
    }
    public class ThemeChangedEventArgs : EventArgs
    {
        public ThemeChangedEventArgs(string newTheme)
        {
            NewTheme = newTheme;
        }
        public string NewTheme { get; private set; }
    }

    public delegate void ThemeChangedEventHandler(object sender, ThemeChangedEventArgs e);
    public static class Theme
    {
        public static event ThemeChangedEventHandler ThemeChanged;

        public static void OnThemeChanged(string newTheme)
        {
            ThemeChanged?.Invoke(null, new ThemeChangedEventArgs(newTheme));
        }
    }
}
