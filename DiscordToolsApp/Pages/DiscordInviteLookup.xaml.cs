using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordToolsApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DiscordInviteLookup : ContentPage
    {
        public DiscordInviteLookup()
        {
            InitializeComponent();
            // Root myDeserializedClass = JsonConvert.DeserializeObject<InviteItems>(myJsonResponse);
        }

    }
    public class InviteBase
    {
        public string code { get; set; }
        public Guild guild { get; set; }
        public Channel channel { get; set; }
        public User inviter { get; set; }
        public int? target_type { get; set; }
        public User target_user { get; set; }
        public Application target_application { get; set; }
        public int? approximate_presence_count { get; set; }
        public int? approximate_member_count { get; set; }
        public DateTime? expires_at { get; set; }
        public StageInstance stage_instance { get; set; }
        public GuildScheduledEvent guild_scheduled_event { get; set; }
        public int uses { get; set; }
        public int max_uses { get; set; }
        public int max_age { get; set; }
        public bool temporary { get; set; }
        public DateTime created_at { get; set; }
    }

    public class Guild
    {
        public ulong id { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public string icon_hash { get; set; }
        public string splash { get; set; }
        public string discovery_splash { get; set; }
        public bool? owner { get; set; }
        public ulong owner_id { get; set; }
        public string permissions { get; set; }
        public string region { get; set; }
        public ulong afk_channel_id { get; set; }
        public int afk_timeout { get; set; }
        public bool? widget_enabled { get; set; }
        public ulong widget_channel_id { get; set; }
        public int verification_level { get; set; }
        public int default_message_notifications { get; set; }
        public int explicit_content_filter { get; set; }
        public List<Role> roles { get; set; }
        public List<Emoji> emojis { get; set; }
        public List<GuildFeatures> features { get; set; }
        public int mfa_level { get; set; }
        public ulong application_id { get; set; }
        public ulong system_channel_id { get; set; }
        public int system_channel_flags { get; set; }
        public ulong rules_channel_id { get; set; }
        public int? max_presences { get; set; }
        public int max_members { get; set; }
        public string vanity_url_code { get; set; }
        public string description { get; set; }
        public string banner { get; set; }
        public int premium_tier { get; set; }
        public int? premium_subscription_count { get; set; }
        public string preferred_locale { get; set; }
        public ulong public_updates_channel_id { get; set; }
        public int max_video_channel_users { get; set; }
        public int max_stage_video_channel_users { get; set; }
        public int approximate_member_count { get; set; }
        public int approximate_presence_count { get; set; }
        public WelcomeScreen welcome_screen { get; set; }
        public int nsfw_level { get; set; }
        public List<Sticker> stickers { get; set; }
        public bool premium_progress_bar_enabled { get; set; }
    }
    public class WelcomeScreen
    {
        public string? description { get; set; }
        public List<WelcomeScreenChannel> welcome_channels { get; set; }
    }
    public class WelcomeScreenChannel
    {
        public ulong channel_id { get; set; }
        public string description { get; set; }
        public ulong? emoji_id { get; set; }
        public string? emoji_name { get; set; }
    }
    public class Sticker
    {
        public ulong id { get; set; }
        public ulong? pack_id { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public string tags { get; set; }
        public string? asset { get; set; }
        public int type { get; set; }
        public int format_type { get; set; }
        public bool? available { get; set; }
        public ulong? guild_id { get; set; }
        public User user { get; set; }
        public int? sort_value { get; set; }
    }
    public class GuildFeatures
    {
        public string ANIMATED_BANNER { get; set; }
        public string ANIMATED_ICON { get; set; }
        public string APPLICATION_COMMAND_PERMISSIONS_V2 { get; set; }
        public string AUTO_MODERATION { get; set; }
        public string BANNER { get; set; }
        public string COMMUNITY { get; set; }
        public string CREATOR_MONETIZABLE_PROVISIONAL { get; set; }
        public string CREATOR_STORE_PAGE { get; set; }
        public string DEVELOPER_SUPPORT_SERVER { get; set; }
        public string DISCOVERABLE { get; set; }
        public string FEATURABLE { get; set; }
        public string INVITES_DISABLED { get; set; }
        public string INVITE_SPLASH { get; set; }
        public string MEMBER_VERIFICATION_GATE_ENABLED { get; set; }
        public string MORE_STICKERS { get; set; }
        public string NEWS { get; set; }
        public string PARTNERED { get; set; }
        public string PREVIEW_ENABLED { get; set; }
        public string ROLE_ICONS { get; set; }
        public string ROLE_SUBSCRIPTIONS_AVAILABLE_FOR_PURCHASE { get; set; }
        public string ROLE_SUBSCRIPTIONS_ENABLED { get; set; }
        public string TICKETED_EVENTS_ENABLED { get; set; }
        public string VANITY_URL { get; set; }
        public string VERIFIED { get; set; }
        public string VIP_REGIONS { get; set; }
        public string WELCOME_SCREEN_ENABLED { get; set; }
    }
    public class Emoji
    {
        public ulong? id { get; set; }
        public string name { get; set; }
        public ulong[] roles { get; set; }
        public User user { get; set; }
        public bool require_colons { get; set; }
        public bool managed { get; set; }
        public bool animated { get; set; }
        public bool available { get; set; }
    }
    public class Role
    {
        public ulong id { get; set; }
        public string name { get; set; }
        public int color { get; set; }
        public bool hoist { get; set; }
        public string icon { get; set; }
        public string unicode_emoji { get; set; }
        public int position { get; set; }
        public string permissions { get; set; }
        public bool managed { get; set; }
        public bool mentionable { get; set; }
        public RoleTags tags { get; set; }
    }
    public class RoleTags
    {
        public ulong? bot_id { get; set; }
        public ulong? integration_id { get; set; }
        public bool? premium_subscriber { get; set; }
        public ulong? subscription_listing_id { get; set; }
        public bool? available_for_purchase { get; set; }
        public bool? guild_connections { get; set; }
    }

    public class Channel
    {
        public ulong id { get; set; }
        public int type { get; set; }
        public ulong? guild_id { get; set; }
        public int? position { get; set; }
        public List<Overwrite> permission_overwrites { get; set; }
        public string? name { get; set; }
        public string? topic { get; set; }
        public bool nsfw { get; set; }
        public ulong? last_message_id { get; set; }
        public int? bitrate { get; set; }
        public int? user_limit { get; set; }
        public int? rate_limit_per_user { get; set; }
        public List<User> recipients { get; set; }
        public string? icon { get; set; }
        public ulong? owner_id { get; set; }
        public ulong? application_id { get; set; }
        public bool managed { get; set; }
        public ulong? parent_id { get; set; }
        public DateTimeOffset? last_pin_timestamp { get; set; }
        public string? rtc_region { get; set; }
        public int video_quality_mode { get; set; }
        public int? message_count { get; set; }
        public int member_count { get; set; }
        public ThreadMetadata thread_metadata { get; set; }
        public ThreadMember member { get; set; }
        public int default_auto_archive_duration { get; set; }
        public string? permissions { get; set; }
        public int flags { get; set; }
        public int total_message_sent { get; set; }
        public List<Tag> available_tags { get; set; }
        public List<ulong> applied_tags { get; set; }
        public DefaultReaction default_reaction_emoji { get; set; }
        public int default_thread_rate_limit_per_user { get; set; }
        public int? default_sort_order { get; set; }
        public int default_forum_layout { get; set; }
    }
    public class Overwrite
    {
        public ulong Id { get; set; }
        public int Type { get; set; }
        public string Allow { get; set; }
        public string Deny { get; set; }
    }
    public class ThreadMetadata
    {
        public bool Archived { get; set; }
        public int AutoArchiveDuration { get; set; }
        public DateTime ArchiveTimestamp { get; set; }
        public bool Locked { get; set; }
        public bool Invitable { get; set; }
        public DateTime? CreateTimestamp { get; set; }
    }
    public class ThreadMember
    {
        public ulong? Id { get; set; }
        public ulong? UserId { get; set; }
        public DateTimeOffset JoinTimestamp { get; set; }
        public int Flags { get; set; }
        public GuildMember Member { get; set; }
    }
    public class GuildMember
    {
        public User user { get; set; }
        public string nick { get; set; }
        public string avatar { get; set; }
        public List<ulong> roles { get; set; }
        public DateTime joined_at { get; set; }
        public DateTime? premium_since { get; set; }
        public bool deaf { get; set; }
        public bool mute { get; set; }
        public int flags { get; set; }
        public bool? pending { get; set; }
        public string permissions { get; set; }
        public DateTime? communication_disabled_until { get; set; }
    }
    public class Tag
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public bool Moderated { get; set; }
        public ulong? EmojiId { get; set; }
        public string EmojiName { get; set; }
    }
    public class DefaultReaction
    {
        public ulong? EmojiId { get; set; }
        public string EmojiName { get; set; }
    }


    public class User
    {
        public ulong id { get; set; }
        public string username { get; set; }
        public string discriminator { get; set; }
        public string avatar { get; set; }
        public bool? bot { get; set; }
        public bool? system { get; set; }
        public bool? mfa_enabled { get; set; }
        public string banner { get; set; }
        public int? accent_color { get; set; }
        public string locale { get; set; }
        public bool? verified { get; set; }
        public string email { get; set; }
        public int? flags { get; set; }
        public int? premium_type { get; set; }
        public int? public_flags { get; set; }
    }

    public class Application
    {
        // properties for the application object
    }

    public class StageInstance
    {
        // properties for the stage instance object
    }

    public class GuildScheduledEvent
    {
        // properties for the guild scheduled event object
    }

}