using System.Text;
using DiscordToolsApp.Components.Partials.Views.CustomItemViews;
using DiscordToolsApp.Helpers;
using Newtonsoft.Json;

namespace DiscordToolsApp.Components.Pages;

public partial class WebhookSendPage : ContentPage
{
    private string mode = "simple";

    public WebhookSendPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        btnSimple.IsEnabled = false;
        btnAdvanced.IsEnabled = true;
        mode = "simple";

        editorJsonContent.Text =
            @"{""content"": ""Hello There!"",""username"": ""Webhook"",""avatar_url"": ""https://i.imgur.com/cy0P10O.png""}";
        editorJsonContent.Text = JsonHelper.BeautifyJson(editorJsonContent.Text);

        base.OnAppearing();
    }

    private async void WebhookRemoteAppLink_Tapped(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(
            "https://play.google.com/store/apps/details?id=com.discordwebhookremote.android"
        );
    }

    private void Control_Clicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        mode = btn.AutomationId;

        switch (mode)
        {
            case "simple":
                btnSimple.IsEnabled = false;
                btnAdvanced.IsEnabled = true;
                break;

            case "advanced":
                btnSimple.IsEnabled = true;
                btnAdvanced.IsEnabled = false;
                break;
        }
    }

    private async void btnSend_Clicked(object sender, EventArgs e)
    {
        btnSend.IsEnabled = false;
        switch (mode)
        {
            case "simple":
                if (string.IsNullOrWhiteSpace(entryWebhookUrlSimple.Text))
                {
                    ApplicationService.ShowCustomAlert("Error!", "Webhook URL is required.", "Ok");
                    break;
                }
                if (string.IsNullOrWhiteSpace(entryContent.Text))
                {
                    ApplicationService.ShowCustomAlert("Error!", "Content is required.", "Ok");
                    break;
                }
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
                        username = string.IsNullOrWhiteSpace(entryName.Text)
                            ? null
                            : entryName.Text,
                        avatar_url = string.IsNullOrWhiteSpace(entryImg.Text)
                            ? null
                            : entryImg.Text,
                    };

                    var json = JsonConvert.SerializeObject(data);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var httpRes = await httpClient.PostAsync(
                            entryWebhookUrlSimple.Text.Trim(),
                            content
                        );
                        if (httpRes.IsSuccessStatusCode)
                        {
                            ApplicationService.ShowCustomAlert(
                                "Success!",
                                "Webhook Message Sent.",
                                "Ok"
                            );
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
                break;

            case "advanced":
                if (string.IsNullOrWhiteSpace(entryWebhookUrlAdvanced.Text))
                {
                    ApplicationService.ShowCustomAlert("Error!", "Webhook URL is required.", "Ok");
                    break;
                }
                if (string.IsNullOrWhiteSpace(editorJsonContent.Text))
                {
                    ApplicationService.ShowCustomAlert("Error!", "Content is required.", "Ok");
                    break;
                }
                if (!JsonHelper.IsValidJson(editorJsonContent.Text))
                {
                    ApplicationService.ShowCustomAlert(
                        "Error!",
                        "Please enter a valid JSON.",
                        "Ok"
                    );
                    break;
                }
                var resQ2 = await ApplicationService.ShowCustomAlertAsync(
                    "Send Message",
                    "Are you sure you want to send this message?",
                    "Yes",
                    "No"
                );
                if (resQ2)
                {
                    ApplicationService.ShowLoadingView();
                    var content = new StringContent(
                        editorJsonContent.Text,
                        Encoding.UTF8,
                        "application/json"
                    );

                    using (var httpClient = new HttpClient())
                    {
                        var httpRes = await httpClient.PostAsync(
                            entryWebhookUrlAdvanced.Text.Trim(),
                            content
                        );
                        if (httpRes.IsSuccessStatusCode)
                        {
                            ApplicationService.ShowCustomAlert(
                                "Success!",
                                "Webhook Message Sent.",
                                "Ok"
                            );
                        }
                        else
                        {
                            ApplicationService.ShowCustomAlert(
                                "Error!",
                                $"An error occurred while sending webhook message.\nStatus: {httpRes.StatusCode}",
                                "Ok"
                            );
                        }
                    }
                }

                break;
        }
        btnSend.IsEnabled = true;
        ApplicationService.HideLoadingView();
    }

    private void btnBeautify_Clicked(object sender, EventArgs e)
    {
        editorJsonContent.Text = JsonHelper.BeautifyJson(editorJsonContent.Text);
    }

    private async void btnClear_Clicked(object sender, EventArgs e)
    {
        btnClear.IsEnabled = false;

        switch (mode)
        {
            case "simple":
                if (string.IsNullOrWhiteSpace(entryContent.Text))
                {
                    btnClear.IsEnabled = true;
                    return;
                }
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
                break;

            case "advanced":
                editorJsonContent.Text = string.Empty;
                break;
        }

        btnClear.IsEnabled = true;
    }

    private void WebhookUrl_TextComplated(object sender, EventArgs e)
    {
        if (sender is null)
            return;

        var entry = sender as CustomEntryView;

        if (entry is null)
            return;

        if (entry.AutomationId is "simple")
            entryWebhookUrlAdvanced.Text = entryWebhookUrlSimple.Text;

        if (entry.AutomationId is "advanced")
            entryWebhookUrlSimple.Text = entryWebhookUrlAdvanced.Text;
    }
}
