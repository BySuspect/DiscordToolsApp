namespace DiscordToolsApp.Helpers
{
    public class DiscordPermissionHelper
    {
        private static readonly Dictionary<string, ulong> permissionMap = new Dictionary<string, ulong>
        {
            {"CREATE_INSTANT_INVITE", 0x0000000000000001},
            {"KICK_MEMBERS", 0x0000000000000002},
            {"BAN_MEMBERS", 0x0000000000000004},
            {"ADMINISTRATOR", 0x0000000000000008},
            {"MANAGE_CHANNELS", 0x0000000000000010},
            {"MANAGE_GUILD", 0x0000000000000020},
            {"ADD_REACTIONS", 0x0000000000000040},
            {"VIEW_AUDIT_LOG", 0x0000000000000080},
            {"PRIORITY_SPEAKER", 0x0000000000000100},
            {"STREAM", 0x0000000000000200},
            {"VIEW_CHANNEL", 0x0000000000000400},
            {"SEND_MESSAGES", 0x0000000000000800},
            {"SEND_TTS_MESSAGES", 0x0000000000001000},
            {"MANAGE_MESSAGES", 0x0000000000002000},
            {"EMBED_LINKS", 0x0000000000004000},
            {"ATTACH_FILES", 0x0000000000008000},
            {"READ_MESSAGE_HISTORY", 0x0000000000010000},
            {"MENTION_EVERYONE", 0x0000000000020000},
            {"USE_EXTERNAL_EMOJIS", 0x0000000000040000},
            {"VIEW_GUILD_INSIGHTS", 0x0000000000080000},
            {"CONNECT", 0x0000000000100000},
            {"SPEAK", 0x0000000000200000},
            {"MUTE_MEMBERS", 0x0000000000400000},
            {"DEAFEN_MEMBERS", 0x0000000000800000},
            {"MOVE_MEMBERS", 0x0000000001000000},
            {"USE_VAD", 0x0000000002000000},
            {"CHANGE_NICKNAME", 0x0000000004000000},
            {"MANAGE_NICKNAMES", 0x0000000008000000},
            {"MANAGE_ROLES", 0x0000000010000000},
            {"MANAGE_WEBHOOKS", 0x0000000020000000},
            {"MANAGE_GUILD_EXPRESSIONS", 0x0000000040000000},
            {"USE_APPLICATION_COMMANDS", 0x0000000080000000},
            {"REQUEST_TO_SPEAK", 0x0000000100000000},
            {"MANAGE_EVENTS", 0x0000000200000000},
            {"MANAGE_THREADS", 0x0000000400000000},
            {"CREATE_PUBLIC_THREADS", 0x0000000800000000},
            {"CREATE_PRIVATE_THREADS", 0x0000001000000000},
            {"USE_EXTERNAL_STICKERS", 0x0000002000000000},
            {"SEND_MESSAGES_IN_THREADS", 0x0000004000000000},
            {"USE_EMBEDDED_ACTIVITIES", 0x0000008000000000},
            {"MODERATE_MEMBERS", 0x0000010000000000},
            {"VIEW_CREATOR_MONETIZATION_ANALYTICS", 0x0000020000000000},
            {"USE_SOUNDBOARD", 0x0000040000000000},
            {"CREATE_GUILD_EXPRESSIONS", 0x0000080000000000},
            {"CREATE_EVENTS", 0x0000100000000000},
            {"USE_EXTERNAL_SOUNDS", 0x0000200000000000},
            {"SEND_VOICE_MESSAGES", 0x0000400000000000},
            {"SEND_POLLS", 0x0002000000000000},
            {"USE_EXTERNAL_APPS", 0x0004000000000000}
        };
        private static readonly Dictionary<string, string> PermissionTitleMap = new Dictionary<string, string>
        {
            {"CREATE_INSTANT_INVITE", "Create Instant Invite"},
            {"KICK_MEMBERS", "Kick Members"},
            {"BAN_MEMBERS", "Ban Members"},
            {"ADMINISTRATOR", "Administrator"},
            {"MANAGE_CHANNELS", "Manage Channels"},
            {"MANAGE_GUILD", "Manage Guild"},
            {"ADD_REACTIONS", "Add Reactions"},
            {"VIEW_AUDIT_LOG", "View Audit Log"},
            {"PRIORITY_SPEAKER", "Priority Speaker"},
            {"STREAM", "Stream"},
            {"VIEW_CHANNEL", "View Channel"},
            {"SEND_MESSAGES", "Send Messages"},
            {"SEND_TTS_MESSAGES", "Send TTS Messages"},
            {"MANAGE_MESSAGES", "Manage Messages"},
            {"EMBED_LINKS", "Embed Links"},
            {"ATTACH_FILES", "Attach Files"},
            {"READ_MESSAGE_HISTORY", "Read Message History"},
            {"MENTION_EVERYONE", "Mention Everyone"},
            {"USE_EXTERNAL_EMOJIS", "Use External Emojis"},
            {"VIEW_GUILD_INSIGHTS", "View Guild Insights"},
            {"CONNECT", "Connect"},
            {"SPEAK", "Speak"},
            {"MUTE_MEMBERS", "Mute Members"},
            {"DEAFEN_MEMBERS", "Deafen Members"},
            {"MOVE_MEMBERS", "Move Members"},
            {"USE_VAD", "Use Voice Activity"},
            {"CHANGE_NICKNAME", "Change Nickname"},
            {"MANAGE_NICKNAMES", "Manage Nicknames"},
            {"MANAGE_ROLES", "Manage Roles"},
            {"MANAGE_WEBHOOKS", "Manage Webhooks"},
            {"MANAGE_GUILD_EXPRESSIONS", "Manage Guild Expressions"},
            {"USE_APPLICATION_COMMANDS", "Use Application Commands"},
            {"REQUEST_TO_SPEAK", "Request to Speak"},
            {"MANAGE_EVENTS", "Manage Events"},
            {"MANAGE_THREADS", "Manage Threads"},
            {"CREATE_PUBLIC_THREADS", "Create Public Threads"},
            {"CREATE_PRIVATE_THREADS", "Create Private Threads"},
            {"USE_EXTERNAL_STICKERS", "Use External Stickers"},
            {"SEND_MESSAGES_IN_THREADS", "Send Messages in Threads"},
            {"USE_EMBEDDED_ACTIVITIES", "Use Embedded Activities"},
            {"MODERATE_MEMBERS", "Moderate Members"},
            {"VIEW_CREATOR_MONETIZATION_ANALYTICS", "View Creator Monetization Analytics"},
            {"USE_SOUNDBOARD", "Use Soundboard"},
            {"CREATE_GUILD_EXPRESSIONS", "Create Guild Expressions"},
            {"CREATE_EVENTS", "Create Events"},
            {"USE_EXTERNAL_SOUNDS", "Use External Sounds"},
            {"SEND_VOICE_MESSAGES", "Send Voice Messages"},
            {"SEND_POLLS", "Send Polls"},
            {"USE_EXTERNAL_APPS", "Use External Apps"}
        };
        public static ulong ConvertPermissionStringsToInteger(List<string> permissions)
        {
            ulong result = 0;

            if (permissions.Contains("ADMINISTRATOR"))
            {
                return permissionMap["ADMINISTRATOR"];
            }

            foreach (var permission in permissions)
            {
                if (permissionMap.ContainsKey(permission))
                {
                    result |= permissionMap[permission];
                }
            }

            return result;
        }

        public static List<string> ConvertIntegerToPermissionStrings(ulong permissions)
        {
            List<string> result = new List<string>();

            foreach (var kvp in permissionMap)
            {
                if ((permissions & kvp.Value) != 0)
                {
                    result.Add(kvp.Key);
                }
            }

            return result;
        }

        public static string GetPermissionTitle(string permissionId)
        {
            if (PermissionTitleMap.TryGetValue(permissionId, out string title))
            {
                return title;
            }
            return permissionId;
        }
        public static List<string> ConvertPermissionIdsToTitles(List<string> permissionIds)
        {
            List<string> titles = new List<string>();

            foreach (var permissionId in permissionIds)
            {
                string title = GetPermissionTitle(permissionId);
                titles.Add(title);
            }

            return titles;
        }
    }
}
