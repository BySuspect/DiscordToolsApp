using Discord.Webhook;
using DiscordToolsApp.Helpers;
using DiscordToolsApp.Pages.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordToolsApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimpleWebhookSenderPage : ContentPage
    {
        public SimpleWebhookSenderPage()
        {
            InitializeComponent();
#if DEBUG
            entryWebhookUrl.Text = "https://discord.com/api/webhooks/1078245792814477362/RAdB0CTZ96Z8hLrjP-YQL2dNK5bZbdi5pd7pIP0MjUL0pvJhN6jDvMocVRC8FaL6gOgz";
            //entryWebhookAvatarUrl.Text = "https://images-ext-1.discordapp.net/external/RP1sWXFQ7d7IWF_ISD-tP87dGlsd1TW4ItY_6kM6D14/https/i.imgur.com/yL9HNRy.png";
#endif
        }
        private async void btnSendButton_Clicked(object sender, EventArgs e)
        {
            Loodinglayout.IsVisible = true;

            #region EntryFormating
            try { entryWebhookUrl.Text = entryWebhookUrl.Text.Trim(); }
            catch { }

            try { entryWebhookAvatarUrl.Text = entryWebhookAvatarUrl.Text.Trim(); }
            catch { }

            try { entryWebhookName.Text = entryWebhookName.Text.Trim(); }
            catch { }

            try { entryWebhookContent.Text = entryWebhookContent.Text.Trim(); }
            catch { }
            #endregion

            try
            {
                if (string.IsNullOrEmpty(entryWebhookUrl.Text))
                {
                    Loodinglayout.IsVisible = false;
                    return;
                }
                DiscordWebhookClient client = new DiscordWebhookClient(entryWebhookUrl.Text.Trim());
                if (!string.IsNullOrEmpty(entryWebhookAvatarUrl.Text) && !string.IsNullOrEmpty(entryWebhookName.Text) && !string.IsNullOrEmpty(entryWebhookContent.Text))
                {
                    await client.SendMessageAsync(
                        text: entryWebhookContent.Text.Trim(),
                        username: entryWebhookName.Text.Trim(),
                        avatarUrl: entryWebhookAvatarUrl.Text.Trim());
                    ToastController.ShowShortToast("Submitted Successfully");
                }
                else if (!string.IsNullOrEmpty(entryWebhookName.Text) && !string.IsNullOrEmpty(entryWebhookContent.Text))
                {
                    await client.SendMessageAsync(
                        text: entryWebhookContent.Text.Trim(),
                        username: entryWebhookName.Text.Trim());
                    ToastController.ShowShortToast("Submitted Successfully");
                }

                else if (!string.IsNullOrEmpty(entryWebhookAvatarUrl.Text) && !string.IsNullOrEmpty(entryWebhookContent.Text))
                {
                    await client.SendMessageAsync(
                                text: entryWebhookContent.Text.Trim(),
                                avatarUrl: entryWebhookAvatarUrl.Text.Trim());
                    ToastController.ShowShortToast("Submitted Successfully");
                }
                else if (!string.IsNullOrEmpty(entryWebhookContent.Text))
                {
                    await client.SendMessageAsync(
                                text: entryWebhookContent.Text.Trim());
                    ToastController.ShowShortToast("Submitted Successfully");
                }
                else
                    ToastController.ShowShortToast("Send Error!");
            }
            catch
            {
                ToastController.ShowShortToast("Send Error!");
            }
            Loodinglayout.IsVisible = false;
        }


        private void DiscordButton_Clicked(object sender, EventArgs e)
        {
            References.discordClicked();
        }
        private void FeedbackButton_Clicked(object sender, EventArgs e)
        {
            References.FeedbackClicked();
        }

        private async void WebhookRemoteAppLink_Tapped(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://play.google.com/store/apps/details?id=com.awgstudios.discordwebhookremoteapp");
        }
    }
}