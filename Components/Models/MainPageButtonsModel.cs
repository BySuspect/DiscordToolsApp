using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordToolsApp.Components.Models
{
    public class MainPageButtonsModel
    {
        public required MainPageButtonsPageTypeModel PageType { get; set; }
        public required string Text { get; set; }
        public required string Image { get; set; }
    }

    public enum MainPageButtonsPageTypeModel
    {
        Empty,
        MainPage,
        UserLookupPage,
        InviteLookupPage,
        ServerLookupPage,
        TimeStampGeneratorPage,
        TextToEmojiPage,
    }
}
