using System.Text;
using DiscordToolsApp.Helpers;
using Newtonsoft.Json;

namespace DiscordToolsApp.Components.Pages;

public partial class WebhookSendPage : ContentPage
{
    public WebhookSendPage()
    {
        InitializeComponent();
    }

    private async void WebhookRemoteAppLink_Tapped(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(
            "https://play.google.com/store/apps/details?id=com.discordwebhookremote.android"
        );
    }

    private async void Control_Clicked(object sender, EventArgs e) { }

    private async void btnSend_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(entryWebhookUrl.Text))
        {
            ApplicationService.ShowCustomAlert("Error!", "Webhook URL is required.", "Ok");
            return;
        }
        if (string.IsNullOrWhiteSpace(entryContent.Text))
        {
            ApplicationService.ShowCustomAlert("Error!", "Content is required.", "Ok");
            return;
        }
        btnSend.IsEnabled = false;
        var resQ = await ApplicationService.ShowCustomAlertAsync(
            "Send Message",
            "Are you sure you want to send this message?",
            "Yes",
            "No"
        );
        if (resQ)
        {
            ApplicationService.ShowLoadingView();
            var data = new
            {
                content = entryContent.Text.Trim(),
                username = string.IsNullOrWhiteSpace(entryName.Text) ? null : entryName.Text,
                avatar_url = string.IsNullOrWhiteSpace(entryImg.Text) ? null : entryImg.Text,
            };

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var httpRes = await httpClient.PostAsync(entryWebhookUrl.Text.Trim(), content);
                if (httpRes.IsSuccessStatusCode)
                {
                    ApplicationService.ShowCustomAlert("Success!", "Webhook Message Sent.", "Ok");
                }
                else
                {
                    ApplicationService.ShowCustomAlert(
                        "Error!",
                        "An error occurred while sending webhook message.",
                        "Ok"
                    );
                }
            }
        }
        btnSend.IsEnabled = true;
        ApplicationService.HideLoadingView();
    }

    private async void btnClear_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(entryContent.Text))
            return;
        btnClear.IsEnabled = false;
        var resQ = await ApplicationService.ShowCustomAlertAsync(
            "Warning!",
            "Are you sure you want to clear content?",
            "Yes",
            "No"
        );
        if (resQ)
        {
            entryContent.Text = string.Empty;
        }
        btnClear.IsEnabled = true;
    }
}
