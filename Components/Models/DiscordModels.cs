using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordToolsApp.Components.Models
{
    public class DiscordInviteModel
    {
        public int type { get; set; }
        public string code { get; set; }
        public DiscordGuildModel? guild { get; set; }
        public DiscordChannelModel? channel { get; set; }
        public DiscordUserModel? inviter { get; set; }
        public int? target_type { get; set; }
        public DiscordUserModel? target_user { get; set; }
        public DiscordApplicationModel? target_application { get; set; }
        public int? approximate_presence_count { get; set; }
        public int? approximate_member_count { get; set; }
        public DateTime? expires_at { get; set; }
        public object? stage_instance { get; set; }
        public object? guild_scheduled_event { get; set; }
    }

    public class DiscordApplicationModel
    {
        public ulong id { get; set; }
        public string name { get; set; }
        public string? icon { get; set; }
        public string description { get; set; }
        public List<string>? rpc_origins { get; set; }
        public bool bot_public { get; set; }
        public bool bot_require_code_grant { get; set; }
        public DiscordUserModel? bot { get; set; }
        public string? terms_of_service_url { get; set; }
        public string? privacy_policy_url { get; set; }
        public DiscordUserModel? owner { get; set; }
        public string summary { get; set; } = string.Empty;
        public string verify_key { get; set; }
        public object? team { get; set; }
        public ulong? guild_id { get; set; }
        public DiscordGuildModel? guild { get; set; }
        public ulong? primary_sku_id { get; set; }
        public string? slug { get; set; }
        public string? cover_image { get; set; }
        public int? flags { get; set; }
        public int? approximate_guild_count { get; set; }
        public List<string>? redirect_uris { get; set; }
        public string? interactions_endpoint_url { get; set; }
        public string? role_connections_verification_url { get; set; }
        public List<string>? tags { get; set; }
        public object? install_params { get; set; }
        public object? integration_types_config { get; set; }
        public string? custom_install_url { get; set; }
    }

    public class DiscordChannelModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public int type { get; set; }
    }

    public class DiscordGuildModel
    {
        public ulong id { get; set; }
        public string name { get; set; }
        public string? icon { get; set; }
        public string? icon_hash { get; set; }
        public string? splash { get; set; }
        public string? discovery_splash { get; set; }
        public bool? owner { get; set; }
        public ulong owner_id { get; set; }
        public string? permissions { get; set; }
        public string? region { get; set; }
        public ulong? afk_channel_id { get; set; }
        public int afk_timeout { get; set; }
        public bool? widget_enabled { get; set; }
        public ulong? widget_channel_id { get; set; }
        public int verification_level { get; set; }
        public int default_message_notifications { get; set; }
        public int explicit_content_filter { get; set; }
        public List<DiscordRoleModel> roles { get; set; }
        public List<DiscordEmojiModel> emojis { get; set; }
        public List<string> features { get; set; }
        public int mfa_level { get; set; }
        public ulong? application_id { get; set; }
        public ulong? system_channel_id { get; set; }
        public int system_channel_flags { get; set; }
        public ulong? rules_channel_id { get; set; }
        public int? max_presences { get; set; }
        public int? max_members { get; set; }
        public string? vanity_url_code { get; set; }
        public string? description { get; set; }
        public string? banner { get; set; }
        public int premium_tier { get; set; }
        public int? premium_subscription_count { get; set; }
        public string preferred_locale { get; set; }
        public ulong? public_updates_channel_id { get; set; }
        public int? max_video_channel_users { get; set; }
        public int? max_stage_video_channel_users { get; set; }
        public int? approximate_member_count { get; set; }
        public int? approximate_presence_count { get; set; }
        public DiscordWelcomeScreenModel? welcome_screen { get; set; }
        public int nsfw_level { get; set; }
        public bool? nsfw { get; set; }
        public List<DiscordStickerModel>? stickers { get; set; }
        public bool premium_progress_bar_enabled { get; set; }
        public ulong? safety_alerts_channel_id { get; set; }
    }

    public class DiscordWelcomeScreenModel
    {
        public string? description { get; set; }
        public List<DiscordWelcomeScreenChannelModel> welcome_channels { get; set; }
    }

    public class DiscordWelcomeScreenChannelModel
    {
        public ulong channel_id { get; set; }
        public string description { get; set; }
        public ulong? emoji_id { get; set; }
        public string? emoji_name { get; set; }
    }

    public class DiscordRoleModel
    {
        public ulong id { get; set; }
        public string name { get; set; }
        public int color { get; set; }
        public bool hoist { get; set; }
        public string? icon { get; set; }
        public string? unicode_emoji { get; set; }
        public int position { get; set; }
        public string permissions { get; set; }
        public bool managed { get; set; }
        public bool mentionable { get; set; }
        public DiscordRoleTagsModel? tags { get; set; }
        public int flags { get; set; }
    }

    public class DiscordRoleTagsModel
    {
        public ulong? bot_id { get; set; }
        public ulong? integration_id { get; set; }
        public bool? premium_subscriber { get; set; } = null;
        public ulong? subscription_listing_id { get; set; }
        public bool? available_for_purchase { get; set; } = null;
        public bool? guild_connections { get; set; } = null;
    }

    public class DiscordEmojiModel
    {
        public ulong? id { get; set; }
        public string? name { get; set; }
        public List<ulong>? roles { get; set; }
        public DiscordUserModel? user { get; set; }
        public bool? require_colons { get; set; }
        public bool? managed { get; set; }
        public bool? animated { get; set; }
        public bool? available { get; set; }
    }

    public class DiscordStickerModel
    {
        public ulong id { get; set; }
        public ulong? pack_id { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public string tags { get; set; }
        public string? asset { get; set; } = string.Empty; // Deprecated field, now an empty string
        public int type { get; set; }
        public int format_type { get; set; }
        public bool? available { get; set; }
        public ulong? guild_id { get; set; }
        public DiscordUserModel? user { get; set; }
        public int? sort_value { get; set; }
    }

    public class DiscordUserModel
    {
        public ulong id { get; set; }
        public string username { get; set; }
        public string? discriminator { get; set; }
        public string? global_name { get; set; }
        public string? avatar { get; set; }
        public bool? bot { get; set; }
        public string? banner { get; set; }
        public string? banner_color { get; set; }
        public int? accent_color { get; set; }
        public string? locale { get; set; }
        public bool? verified { get; set; }
        public int? flags { get; set; }
        public int? premium_type { get; set; }
        public int? public_flags { get; set; }
        public DiscordAvatarDecorationModel? avatar_decoration_data { get; set; }
    }

    public class DiscordAvatarDecorationModel
    {
        public string asset { get; set; }
        public ulong sku_id { get; set; }
    }
}
