using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordToolsApp.Components.Models
{
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
