using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DiscordToolsApp.Components.Popups.Common;

using Newtonsoft.Json.Linq;

namespace DiscordToolsApp.Services
{
    internal class AppVersionCheckService
    {
        public static async Task CheckVersion()
        {
            try
            {
                string[] serverVersion = (await GetLatestReleaseVersion())
                    .ToString()
                    .Replace("V", "")
                    .Split('.');
                string[] currentVersion = AppInfo.VersionString.Split('.');

                if (int.Parse(serverVersion[0]) > int.Parse(currentVersion[0]))
                {
                    ApplicationService.ShowPopup(
                        new NewVersionNotifyPopup(string.Join(".", serverVersion))
                    );
                }
                else if (int.Parse(serverVersion[0]) == int.Parse(currentVersion[0]))
                {
                    if (int.Parse(serverVersion[1]) > int.Parse(currentVersion[1]))
                    {
                        ApplicationService.ShowPopup(
                            new NewVersionNotifyPopup(string.Join(".", serverVersion))
                        );
                    }
                    else if (int.Parse(serverVersion[1]) == int.Parse(currentVersion[1]))
                    {
                        if (int.Parse(serverVersion[2]) > int.Parse(currentVersion[2]))
                        {
                            ApplicationService.ShowPopup(
                                new NewVersionNotifyPopup(string.Join(".", serverVersion))
                            );
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static async Task<string> GetLatestReleaseVersion()
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl =
                    $"https://api.github.com/repos/BySuspect/DiscordToolsApp/releases/latest";

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject jsonObj = JObject.Parse(responseBody);
                    JToken tagToken = jsonObj["tag_name"];
                    if (tagToken != null)
                    {
                        return tagToken.ToString();
                    }
                }

                return null;
            }
        }
    }
}
