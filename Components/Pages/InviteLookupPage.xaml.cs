using System.Text.RegularExpressions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiscordToolsApp.Components.Pages;

public partial class InviteLookupPage : ContentPage
{
    public InviteLookupPage()
    {
        InitializeComponent();
    }

    private async void btnLookup_Clicked(object sender, EventArgs e)
    {
        ApplicationService.ShowLoadingView();
        btnLookup.IsEnabled = false;
        try
        {
            string invUrl = entryInvite.Text;

            if (string.IsNullOrWhiteSpace(invUrl) || !isInviteUrl(invUrl))
            {
                btnLookup.IsEnabled = true;
                ApplicationService.HideLoadingView();
                ApplicationService.ShowCustomAlert(
                    "Error!",
                    "Invalid Invite URL. Please enter a valid Invite URL.",
                    "Ok"
                );
                return;
            }
            string invCode = invUrl.Split("/").Last();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add(
                "Authorization",
                $"Bot {StaticPropertiesService.DiscordBotApiKey}"
            );

            var res = await client.GetAsync(
                $"https://discord.com/api/invites/{invCode}?with_counts=true&with_expiration=true"
            );

            if (res.IsSuccessStatusCode)
            {
                var resData = await res.Content.ReadAsStringAsync();
                //var invData = JsonConvert.DeserializeObject<DiscordInviteModel>(resData);

                //var createdat = GetTimestampFromSnowflake(invData.id)
                //    .ToString("dddd, MMM dd, yyyy hh:mm tt");
            }
            else
            {
                //userDetailView.IsVisible = false;
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
            //userDetailView.IsVisible = false;
            ApplicationService.ShowCustomAlert("Error!", ex.Message, "Ok");
        }
        btnLookup.IsEnabled = true;
        ApplicationService.HideLoadingView();
    }

    private static bool isInviteUrl(string url)
    {
        string pattern =
            @"(?:https?:\/\/)?(?:www\.)?discord(?:app)?\.com\/invite\/[a-zA-Z0-9\-]{2,}";
        return Regex.IsMatch(url, pattern);
    }

    public TimeSpan CalculateRemainingTime(DateTime targetDateTime)
    {
        DateTime now = DateTime.Now;
        TimeSpan remainingTime = targetDateTime - now;

        return remainingTime;
    }

    private static DateTime GetTimestampFromSnowflake(ulong snowflake)
    {
        const long DiscordEpoch = 1420070400000L;
        const ulong TimestampMask = 0xFFFFFFFFFFC00000UL;

        long timestamp = (long)((snowflake & TimestampMask) >> 22) + DiscordEpoch;

        DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime;

        return dateTime;
    }
}
