using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordToolsApp.Components.Models
{
    public class DiscordInviteModel
    {
        public int type { get; set; }
        public string code { get; set; }
        public Guild guild { get; set; }
        public Channel channel { get; set; }
        public DiscordUserModel inviter { get; set; }
        public int target_type { get; set; }
        public DiscordUserModel target_user { get; set; }

        public class Channel
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
        }

        public class Guild
        {
            public string id { get; set; }
            public string name { get; set; }
            public object splash { get; set; }
            public object banner { get; set; }
            public string description { get; set; }
            public object icon { get; set; }
            public List<string> features { get; set; }
            public int verification_level { get; set; }
            public object vanity_url_code { get; set; }
            public int nsfw_level { get; set; }
            public int premium_subscription_count { get; set; }
        }
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
        public int? accent_color { get; set; }
        public string? locale { get; set; }
        public bool? verified { get; set; }
        public int? flags { get; set; }
        public int? premium_type { get; set; }
        public int? public_flags { get; set; }
        public AvatarDecorationData? avatar_decoration_data { get; set; }
    }

    public class AvatarDecorationData
    {
        public string asset { get; set; }
        public ulong sku_id { get; set; }
    }
}
