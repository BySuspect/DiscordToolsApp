using System.Linq;
using System.Text.RegularExpressions;
using DiscordToolsApp.Components.Models;
using DiscordToolsApp.Components.Popups.Common;
using DiscordToolsApp.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiscordToolsApp.Components.Pages;

public partial class InviteLookupPage : ContentPage
{
    public InviteLookupPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        InviteDetailView.IsVisible = false;
        base.OnAppearing();
    }

    private async void btnLookup_Clicked(object sender, EventArgs e)
    {
        ApplicationService.ShowLoadingView();
        btnLookup.IsEnabled = false;
        entryInvite.Unfocus();
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
            //TODO: needs testings
            string invCode = invUrl.Trim().Split("/").Last().Trim();

            ApplicationService.ShowCustomAlert(
                "Info.",
                "Some times images may not be animated.",
                "Ok"
            );

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add(
                "Authorization",
                $"Bot {EncryptionHelper.Decrypt(StaticPropertiesService.DiscordBotApiKey)}"
            );

            var res = await client.GetAsync(
                $"https://discord.com/api/invites/{invCode}?with_counts=true&with_expiration=true"
            );

            if (res.IsSuccessStatusCode)
            {
                var resData = await res.Content.ReadAsStringAsync();
                var invData = JsonConvert.DeserializeObject<DiscordInviteModel>(resData);

                if (invData is null)
                    return;

                if (invData.guild is not null)
                {
                    if (invData.guild.icon is not null)
                    {
                        var icon =
                            $"https://cdn.discordapp.com/icons/{invData.guild.id}/{invData.guild.icon}?size=256";
                        imgIcon.Source = icon;
                        iconView.IsVisible = true;
                    }
                    else
                    {
                        imgIcon.Source = string.Empty;
                        iconView.IsVisible = false;
                    }

                    if (invData.guild.banner is not null)
                    {
                        var banner =
                            $"https://cdn.discordapp.com/banners/{invData.guild.id}/{invData.guild.banner}?size=256";
                        imgBanner.Source = banner;
                        bannerView.IsVisible = true;
                    }
                    else
                    {
                        imgBanner.Source = string.Empty;
                        bannerView.IsVisible = false;
                    }

                    if (invData.guild.splash is not null)
                    {
                        var splash =
                            $"https://cdn.discordapp.com/splashes/{invData.guild.id}/{invData.guild.splash}?size=512";
                        imgSplash.Source = splash;
                        splashView.IsVisible = true;
                    }
                    else
                    {
                        imgSplash.Source = string.Empty;
                        splashView.IsVisible = false;
                    }
                }

                InvType.Value =
                    invData.type == 0
                        ? "Guild"
                        : invData.type == 1
                            ? "Group DM"
                            : invData.type == 2
                                ? "Friend DM"
                                : "Unknown";

                InvlinkView.Value = invUrl;

                VanitylinkView.Value =
                    (string.IsNullOrWhiteSpace(invData.guild.vanity_url_code))
                        ? "Not Have"
                        : $"https://discord.gg/{invData.guild.vanity_url_code}";

                TRView.Value =
                    (invData.expires_at is null)
                        ? "Unlimited"
                        : CalculateRemainingTime(invData.expires_at).TotalDays.ToString("#")
                            + " days";

                SIdView.Value = invData.guild.id.ToString();

                SNameView.Value = invData.guild.name;

                SDescView.Value = invData.guild.description ?? string.Empty;

                SCAView.Value = GetTimestampFromSnowflake(invData.guild.id)
                    .ToString("dddd, MMM dd, yyyy hh:mm tt");

                SFeatView.Value = string.Join(", ", invData.guild.features);

                SVerifView.Value =
                    invData.guild.verification_level == 0
                        ? "Unrestricted"
                        : invData.guild.verification_level == 1
                            ? "Must have verified email on account"
                            : invData.guild.verification_level == 2
                                ? "Must be registered on Discord for longer than 5 minutes"
                                : invData.guild.verification_level == 3
                                    ? "Must be a member of the server for longer than 10 minutes"
                                    : invData.guild.verification_level == 4
                                        ? "Must have a verified phone number"
                                        : "Unknown";

                SMCountView.Value = invData.approximate_member_count.ToString() ?? "Invaild";

                SOMCountView.Value = invData.approximate_presence_count.ToString() ?? "Invaild";

                SNCountView.Value =
                    invData.guild.premium_subscription_count.ToString() ?? "Invaild";

                NSFWEnabledView.Value = invData.guild.nsfw ?? false ? "Enabled" : "Disabled";

                NSFWLevelView.Value =
                    invData.guild.nsfw_level == 0
                        ? "Default"
                        : invData.guild.nsfw_level == 1
                            ? "Explicit"
                            : invData.guild.nsfw_level == 2
                                ? "Safe"
                                : invData.guild.nsfw_level == 3
                                    ? "Age Restricted"
                                    : "Unknown";

                if (invData.channel is not null)
                {
                    SCId.Value = invData.channel.id.ToString();
                    SCName.Value = invData.channel.name;
                    SCType.Value =
                        invData.channel.type == 0
                            ? "Text"
                            : invData.channel.type == 1
                                ? "DM"
                                : invData.channel.type == 2
                                    ? "Voice"
                                    : invData.channel.type == 3
                                        ? "Group DM"
                                        : invData.channel.type == 5
                                            ? "News"
                                            : "Unknown";
                }
                else
                {
                    SCId.Value = string.Empty;
                    SCName.Value = string.Empty;
                    SCType.Value = string.Empty;
                }

                if (invData.inviter is not null)
                {
                    inviterDetailsView.User = invData.inviter;
                    inviterDetailsView.IsVisible = true;
                }
                else
                    inviterDetailsView.IsVisible = false;

                InviteDetailView.IsVisible = true;
                entryInvite.Text = string.Empty;
            }
            else
            {
                InviteDetailView.IsVisible = false;
                var errorData = await res.Content.ReadAsStringAsync();
                if (errorData is not null)
                {
                    var error = JsonConvert.DeserializeObject<JObject>(errorData);
                    ApplicationService.ShowCustomAlert(
                        "Error!",
                        $"Something went wrong when attempting to get invite details.\n\nResponse: {error["message"] ?? "-empty-"}",
                        "Ok"
                    );
                }
                else
                    ApplicationService.ShowCustomAlert(
                        "Error!",
                        $"Something went wrong when attempting to get invite details.\n\nResponse: {res.RequestMessage}",
                        "Ok"
                    );
            }
        }
        catch (Exception ex)
        {
            InviteDetailView.IsVisible = false;
            ApplicationService.ShowCustomAlert("Error!", ex.Message, "Ok");
        }
        btnLookup.IsEnabled = true;
        ApplicationService.HideLoadingView();
    }

    private void Icon_Tapped(object sender, TappedEventArgs e)
    {
        var icon = imgIcon.Source.Split('?')[0] + "?size=1024";
        ApplicationService.ShowPopup(new ImageViewPopup(icon));
    }

    private void Banner_Tapped(object sender, TappedEventArgs e)
    {
        var banner = imgBanner.Source.Split('?')[0] + "?size=1024";
        ApplicationService.ShowPopup(new ImageViewPopup(banner));
    }

    private void Splash_Tapped(object sender, TappedEventArgs e)
    {
        var splash = imgSplash.Source.Split('?')[0] + "?size=1024";
        ApplicationService.ShowPopup(new ImageViewPopup(splash));
    }

    private static bool isInviteUrl(string url)
    {
        string pattern = @"(https?://)?(www.)?(discord.gg|discordapp.com|discord.com/invite)/*";
        return Regex.IsMatch(url, pattern);
    }

    public TimeSpan CalculateRemainingTime(DateTime? targetDateTime)
    {
        if (targetDateTime is null)
            return new TimeSpan();

        DateTime now = DateTime.Now;
        TimeSpan remainingTime = (TimeSpan)(targetDateTime - now);

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
