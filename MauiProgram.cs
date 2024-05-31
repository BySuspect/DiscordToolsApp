using System.Reflection;
using System.Runtime.CompilerServices;

using CommunityToolkit.Maui;

using DiscordToolsApp.Handlers;
using DiscordToolsApp.Helpers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Plugin.MauiMTAdmob;

using Syncfusion.Maui.Core.Hosting;

namespace DiscordToolsApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiMTAdmob()
                .ConfigureSyncfusionCore()
                .UseMauiCommunityToolkit();

            builder.AddEnvJson();

            FormHandler.RemoveBorders();
            AppThemeService.SetTheme(AppThemeTypes.Discord);

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
                builder.Configuration.GetValue<string>("SYNCFUSION_KEY")
            );

            StaticPropertiesService.DiscordBotApiKey = EncryptionHelper.Encrypt(
                builder.Configuration.GetValue<string>("DISCORD_API_KEY")
            );
            StaticPropertiesService.SuggestIdeaWebhookUrl = EncryptionHelper.Encrypt(
                builder.Configuration.GetValue<string>("SUGGEST_IDEA_WEBHOOK_URL")
            );

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void AddEnvJson(this MauiAppBuilder builder)
        {
            using Stream? stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("DiscordToolsApp.env.json");
            if (stream != null)
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();
                builder.Configuration.AddConfiguration(config);
            }
        }
    }
}
