using System.Text;

using CommunityToolkit.Maui.Views;

using DiscordToolsApp.Helpers;

using Newtonsoft.Json;

namespace DiscordToolsApp.Components.Popups.Feedback;

public partial class SuggestToolIdePopup : Popup
{
    public SuggestToolIdePopup()
    {
        InitializeComponent();
    }

    private async void btnSend_Clicked(object sender, EventArgs e)
    {
        btnSend.IsEnabled = false;
        if (string.IsNullOrEmpty(entryTitle.Text) || string.IsNullOrWhiteSpace(entryContent.Text))
        {
            if (
                string.IsNullOrEmpty(entryTitle.Text)
                && string.IsNullOrWhiteSpace(entryContent.Text)
            )
            {
                ApplicationService.ShowCustomAlert(
                    "Warning!",
                    "Please enter a title and content.",
                    "Ok"
                );
            }
            else if (string.IsNullOrEmpty(entryTitle.Text))
            {
                ApplicationService.ShowCustomAlert("Warning!", "Please enter a title.", "Ok");
            }
            else if (string.IsNullOrWhiteSpace(entryContent.Text))
            {
                ApplicationService.ShowCustomAlert("Warning!", "Please enter a content.", "Ok");
            }

            btnSend.IsEnabled = true;
            return;
        }

        var res = await ApplicationService.ShowCustomAlertAsync(
            "Warning!",
            "You can send Daily 1 Suggestion, do you want send it now?",
            "Yes",
            "No"
        );

        if (res)
        {
            ApplicationService.ShowLoadingView();
            var data = new
            {
                embeds = new[]
                {
                    new
                    {
                        title = entryTitle.Text.Trim(),
                        description = entryContent.Text.Trim(),
                        color = 10364355,
                        timestamp = DateTime.UtcNow.ToString("o")
                    }
                }
            };

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var httpRes = await httpClient.PostAsync(
                    EncryptionHelper.Decrypt(StaticPropertiesService.SuggestIdeaWebhookUrl),
                    content
                );
                if (httpRes.IsSuccessStatusCode)
                {
                    Preferences.Set("toolSuggestDate", DateTime.Now);
                    ApplicationService.ShowCustomAlert(
                        "Success!",
                        "Your suggestion has been sent.",
                        "Ok"
                    );
                }
                else
                {
                    ApplicationService.ShowCustomAlert(
                        "Error!",
                        "An error occurred while sending the suggestion.",
                        "Ok"
                    );
                    btnSend.IsEnabled = true;
                    ApplicationService.HideLoadingView();
                    return;
                }
            }
        }
        else
        {
            btnSend.IsEnabled = true;
            return;
        }
        ApplicationService.HideLoadingView();
        this.Close();
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        this.Close();
    }
}
