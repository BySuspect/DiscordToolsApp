﻿using CommunityToolkit.Maui;
using DiscordToolsApp.Handlers;
using Microsoft.Extensions.Logging;
using Plugin.MauiMTAdmob;

namespace DiscordToolsApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().UseMauiMTAdmob().UseMauiCommunityToolkit();

            FormHandler.RemoveBorders();
            AppThemeService.SetTheme(AppThemeTypes.Discord);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
