using Android.Widget;
using DiscordToolsApp.Droid.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(Toast_Android))]
namespace DiscordToolsApp.Droid.Helpers
{
    public class Toast_Android : DiscordToolsApp.Helpers.Toast
    {
        public void ShowLong(string message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
        public void ShowShort(string message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}