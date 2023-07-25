using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DiscordToolsApp.Helpers
{
    public static class Logger
    {
        private static string uid;

        public static void LogMessage(string message, LogLevel logLevel = LogLevel.Info)
        {
            var logEntry = new LogEntry
            {
                Timestamp = getTimestamp(),
                Level = logLevel,
                Message = message
            };

            WriteLogEntry(logEntry);
        }

        private static void WriteLogEntry(LogEntry logEntry)
        {
            try
            {
                List<LogEntry> tempLogEntries;
                List<LogEntry> logEntries;
                tempLogEntries = JsonConvert.DeserializeObject<List<LogEntry>>(Preferences.Get("DiscordToolsTempLogs", "[ ]"));
                logEntries = JsonConvert.DeserializeObject<List<LogEntry>>(Preferences.Get("DiscordToolsLogs", "[ ]"));

                tempLogEntries.Add(logEntry);
                logEntries.Add(logEntry);

                var tempjsonData = JsonConvert.SerializeObject(tempLogEntries);
                var jsonData = JsonConvert.SerializeObject(tempLogEntries);


                Preferences.Set("DiscordToolsTempLogs", tempjsonData);
                Preferences.Set("DiscordToolsLogs", jsonData);

                if (tempLogEntries.Where(x => x.Level == LogLevel.Error).ToList().Count() > 5)
                {
                    sendLog(tempjsonData);
                }
                Debug.WriteLine($"TempLog: {tempLogEntries.Count()}, Log: {logEntries.Count()}");
            }
            catch (Exception ex)
            {
                // Handle any potential errors while writing to the log file
                //LogMessage("An error occurred while writing to the log file: " + ex.Message, LogLevel.Error);
                Debug.WriteLine("An error occurred while writing to the log file: " + ex.Message);
            }
        }
        private static async void sendLog(string content)
        {
            if (!await References.CheckConnection())
                return;

            try
            {
                if (Preferences.Get("user_feedback_ID", "none") == "none")
                {
                    uid = Guid.NewGuid().ToString();
                    Preferences.Set("user_feedback_ID", uid);
                }
                else
                {
                    uid = Preferences.Get("user_feedback_ID", "none");
                }

                string postUri = "";
#if DEBUG
                postUri = "https://awgstudiosapps-default-rtdb.europe-west1.firebasedatabase.app/Debug/AutoLog/";
#endif
#if !DEBUG
                postUri = "https://awgstudiosapps-default-rtdb.europe-west1.firebasedatabase.app/DiscordToolsFeedback/AutoLog/";
#endif

                // Device Model (SMG-950U, iPhone11,6)
                string device = DeviceInfo.Model;

                // Manufacturer (Samsung)
                string manufacturer = DeviceInfo.Manufacturer;

                // Device Name (Motz's iPhone)
                string deviceName = DeviceInfo.Name;

                // Operating System Version Number (7.0)
                string version = DeviceInfo.VersionString;

                // Platform (Android)
                string platform = DeviceInfo.Platform.ToString();

                // Idiomatic Device Type (Tablet)
                string deviceType = DeviceInfo.Idiom.ToString();

                var httpClient = new HttpClient();
                string requestBody = JsonConvert.SerializeObject(new RootLogContent
                {
                    Content = content,
                    Device = device,
                    Manufacturer = manufacturer,
                    DeviceName = deviceName,
                    Version = version,
                    DeviceType = deviceType,
                    Platform = platform,
                    Timestramp = getTimestamp(),
                });
                HttpContent httpcontent = new StringContent(requestBody, Encoding.UTF8, "application/json");
                if (uid == "none")
                {
                    uid = Guid.NewGuid().ToString();
                    Preferences.Set("user_feedback_ID", uid);
                }
                var res = await httpClient.PostAsync(postUri + uid + "/.json?auth=VjX98JeBVbUmZviKQOrW7yv8NPT69VRzG3m7sXNl", httpcontent);
                if (res.IsSuccessStatusCode)
                {
                    Preferences.Set("DiscordToolsTempLogs", "[ ]");
                }
                else
                {
                    LogMessage("An error occurred while sending to the log file: " + res.Content, LogLevel.Error);
                }
                Debug.WriteLine("Log Send.");
            }
            catch (Exception ex)
            {
                //LogMessage("An error occurred while sending to the log file: " + ex.Message, LogLevel.Error);
                Debug.WriteLine("\n\n\n\n\n------------------------\nAn error occurred while sending to the log file " + ex.Message + "\n------------------------\n\n\n\n\n");
            }
        }

        private static long getTimestamp()
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan timeSpan = DateTime.UtcNow.ToUniversalTime() - unixEpoch;
            return (long)timeSpan.TotalSeconds;
        }

        class RootLogContent
        {
            public string Content { get; set; }
            public string Device { get; set; }
            public string Manufacturer { get; set; }
            public string DeviceName { get; set; }
            public string Version { get; set; }
            public string Platform { get; set; }
            public string DeviceType { get; set; }
            public long Timestramp { get; set; }
        }
    }

    public enum LogLevel
    {
        Info,
        Warning,
        Error
    }

    public class LogEntry
    {
        public long Timestamp { get; set; }
        public LogLevel Level { get; set; }
        public string Message { get; set; }
    }
}
