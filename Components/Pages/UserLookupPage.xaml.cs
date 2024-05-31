using DiscordToolsApp.Components.Models;
using DiscordToolsApp.Helpers;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiscordToolsApp.Components.Pages;

public partial class UserLookupPage : ContentPage
{
    public UserLookupPage()
    {
        InitializeComponent();
    }

    private async void btnLookup_Clicked(object sender, EventArgs e)
    {
        ApplicationService.ShowLoadingView();
        btnLookup.IsEnabled = false;
        entryId.Unfocus();
        ulong uID = 0;
        try
        {
            try
            {
                uID = ulong.Parse(entryId.Text);
            }
            catch
            {
                btnLookup.IsEnabled = true;
                ApplicationService.HideLoadingView();
                ApplicationService.ShowCustomAlert(
                    "Error!",
                    "Invalid User ID. Please enter a valid User ID.",
                    "Ok"
                );
                return;
            }

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add(
                "Authorization",
                $"Bot {EncryptionHelper.Decrypt(StaticPropertiesService.DiscordBotApiKey)}"
            );

            var res = await client.GetAsync($"https://discord.com/api/users/{uID}");

            if (res.IsSuccessStatusCode)
            {
                var resData = await res.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<DiscordUserModel>(resData);

                userDetailView.User = user;

                userDetailView.IsVisible = true;
                entryId.Text = string.Empty;
            }
            else
            {
                userDetailView.IsVisible = false;
                var errorData = await res.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<JObject>(errorData);
                ApplicationService.ShowCustomAlert(
                    "Error!",
                    $"Something went wrong when attempting to get user details.\n\nResponse: {error["message"] ?? "-empty-"}",
                    "Ok"
                );
            }
        }
        catch (Exception ex)
        {
            userDetailView.IsVisible = false;
            ApplicationService.ShowCustomAlert("Error!", ex.Message, "Ok");
        }
        btnLookup.IsEnabled = true;
        ApplicationService.HideLoadingView();
    }
}
