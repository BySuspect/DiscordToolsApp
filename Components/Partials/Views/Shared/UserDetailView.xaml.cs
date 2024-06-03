using DiscordToolsApp.Components.Models;
using DiscordToolsApp.Components.Popups.Common;

using DiscordWebhookRemoteApp.Services;

namespace DiscordToolsApp.Components.Partials.Views.Shared;

public partial class UserDetailView : ContentView
{
    public static readonly BindableProperty UserProperty = BindableProperty.Create(
        nameof(User),
        typeof(DiscordUserModel),
        typeof(UserDetailView),
        null,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            if (newValue is null)
                return;

            var control = (UserDetailView)bindable;
            var user = (DiscordUserModel)newValue;

            var createdat = GetTimestampFromSnowflake(user.id)
                .ToString("dddd, MMM dd, yyyy hh:mm tt");

            control.uidView.Value = user.id.ToString();
            control.caView.Value = createdat;
            control.unView.Value =
                user.username + (user.discriminator != null ? "#" + user.discriminator : "");
            control.gnView.Value = user.global_name ?? user.username;

            var badges = ConvertFlagsToList(user.flags ?? 0).ToArray();
            if ((badges.Length == 1 && badges[0] is "verified_bot_"))
                control.badgesView.Value = null;
            else
                control.badgesView.Value = badges;

            if (user.bot is true)
                if (badges.Contains("verified_bot_"))
                    control.ibView.Value = "Verified Bot";
                else
                    control.ibView.Value = "Unverified Bot";
            else
                control.ibView.Value = "User";

            if (user.avatar is not null)
            {
                var avatar = $"https://cdn.discordapp.com/avatars/{user.id}/{user.avatar}?size=128";
                control.imgAvatar.Source = avatar;
            }
            else
                control.imgAvatar.Source = string.Empty;

            if (user.banner is not null)
            {
                var banner = $"https://cdn.discordapp.com/banners/{user.id}/{user.banner}?size=512";
                control.imgBanner.Source = banner;
                control.BannerView.IsVisible = true;
            }
            else
            {
                control.imgBanner.Source = string.Empty;
                control.BannerView.IsVisible = false;
            }

            if (user.banner_color is not null && user.banner_color is not "#000000")
                control.BColorView.Value = user.banner_color.ToUpper();
            else
                control.BColorView.Value = string.Empty;

            if (
                user.accent_color is not null
                && DecodeAccentColor((int)user.accent_color).ToHex() is not "#000000"
            )
                control.AColorView.Value = DecodeAccentColor((int)user.accent_color).ToHex();
            else
                control.AColorView.Value = string.Empty;
        }
    );

    public DiscordUserModel User
    {
        get { return (DiscordUserModel)GetValue(UserProperty); }
        set { SetValue(UserProperty, value); }
    }

    public UserDetailView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void Avatar_Tapped(object sender, TappedEventArgs e)
    {
        ApplicationService.ShowPopup(
            new ImageViewPopup(imgAvatar.Source.Split('?')[0] + "?size=1024")
        );
    }

    private void Banner_Tapped(object sender, TappedEventArgs e)
    {
        ApplicationService.ShowPopup(
            new ImageViewPopup(imgBanner.Source.Split('?')[0] + "?size=1024")
        );
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
