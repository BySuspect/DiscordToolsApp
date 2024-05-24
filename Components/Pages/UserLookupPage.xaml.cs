using DiscordToolsApp.Components.Models;

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
                $"Bot {StaticPropertiesService.DiscordBotApiKey}"
            );

            var res = await client.GetAsync($"https://discord.com/api/users/{uID}");

            if (res.IsSuccessStatusCode)
            {
                var resData = await res.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<DiscordUserModel>(resData);

                var createdat = GetTimestampFromSnowflake(uID)
                    .ToString("dddd, MMM dd, yyyy hh:mm tt");

                uidView.Value = uID.ToString();
                caView.Value = createdat;
                unView.Value = user.username;
                gnView.Value = user.global_name ?? user.username;

                var badges = ConvertFlagsToList(user.flags ?? 0).ToArray();
                badgesView.Value = badges;

                if (user.bot is true)
                    if (badges.Contains("verified_bot_"))
                        ibView.Value = "Verified Bot";
                    else
                        ibView.Value = "Unverified Bot";
                else
                    ibView.Value = "User";

                if (user.avatar is not null)
                    imgView.Avatar =
                        $"https://cdn.discordapp.com/avatars/{user.id}/{user.avatar}?size=128";
                else
                    imgView.Avatar = string.Empty;

                if (user.banner is not null)
                    imgView.Banner =
                        $"https://cdn.discordapp.com/banners/{user.id}/{user.banner}?size=512";
                else
                    imgView.Banner = string.Empty;

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

    public static Color DecodeAccentColor(int hexColor)
    {
        int red = (hexColor >> 16) & 0xFF;
        int green = (hexColor >> 8) & 0xFF;
        int blue = hexColor & 0xFF;

        return Color.FromRgb(red, green, blue);
    }

    public static DateTime GetTimestampFromSnowflake(ulong snowflake)
    {
        const long DiscordEpoch = 1420070400000L;
        const ulong TimestampMask = 0xFFFFFFFFFFC00000UL;

        long timestamp = (long)((snowflake & TimestampMask) >> 22) + DiscordEpoch;

        DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime;

        return dateTime;
    }

    public static List<string> ConvertFlagsToList(int flags)
    {
        List<string> flagList = new List<string>();

        if ((flags & (1 << 0)) != 0)
            flagList.Add("badges/staff.png");
        if ((flags & (1 << 1)) != 0)
            flagList.Add("badges/partner.png");
        if ((flags & (1 << 2)) != 0)
            flagList.Add("badges/hypesquad_events.png");
        if ((flags & (1 << 3)) != 0)
            flagList.Add("badges/bug_hunter_level1.png");
        if ((flags & (1 << 6)) != 0)
            flagList.Add("badges/hypesquad_bravery.png");
        if ((flags & (1 << 7)) != 0)
            flagList.Add("badges/hypesquad_brilliance.png");
        if ((flags & (1 << 8)) != 0)
            flagList.Add("badges/hypesquad_balance.png");
        if ((flags & (1 << 9)) != 0)
            flagList.Add("badges/early_supporter.png");
        //if ((flags & (1 << 10)) != 0)
        //    flagList.Add("TEAM_PSEUDO_USER");
        if ((flags & (1 << 14)) != 0)
            flagList.Add("badges/bug_hunter_level2.png");
        if ((flags & (1 << 16)) != 0)
            flagList.Add("verified_bot_");
        if ((flags & (1 << 17)) != 0)
            flagList.Add("badges/verified_developer.png");
        if ((flags & (1 << 18)) != 0)
            flagList.Add("badges/certified_moderator.png");
        //if ((flags & (1 << 19)) != 0)
        //    flagList.Add("BOT_HTTP_INTERACTIONS");
        if ((flags & (1 << 22)) != 0)
            flagList.Add("badges/active_developer.png");

        return flagList;
    }
}
