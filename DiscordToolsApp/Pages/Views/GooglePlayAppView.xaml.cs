using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordToolsApp.Pages.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GooglePlayAppView : ContentView
    {
        public GooglePlayAppView()
        {
            InitializeComponent();
        }

        private async void Open_Tapped(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://play.google.com/store/apps/details?id=com.awgstudios.discordwebhookremoteapp");
        }
    }
}