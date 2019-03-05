using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Starcounter.Internal;

namespace UniformDocs.Tests.Utilities
{
    public static class RestApiHelper
    {
        private static readonly ushort InternalPort = StarcounterEnvironment.Default.SystemHttpPort;
        private const string InternalHost = "127.0.0.1";

        public class DatabaseApplicationsJson
        {
            public IEnumerable<App> Items { get; set; }
        }

        public class Log
        {
            public IEnumerable<LogEntry> LogEntries { get; set; }
        }

        public class App
        {
            public string DisplayName { get; set; }
        }

        public class LogEntry
        {
            public string Message { get; set; }
        }

        public static async Task<bool> CheckAppRunning(string appName)
        {
            HttpResponseMessage response = await new HttpClient().GetAsync($"http://{InternalHost}:{InternalPort}/api/admin/databases/default/applications");

            if (response.IsSuccessStatusCode)
            {
                var runningAppsString = await response.Content.ReadAsStringAsync();
                var runningApps = JsonConvert.DeserializeObject<DatabaseApplicationsJson>(runningAppsString);

                foreach (var app in runningApps.Items)
                {
                    if (app.DisplayName == appName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static async Task<string> GetLatestLogEntry()
        {
            string Parameters = "debug=false&error=true&maxitems=1&notice=false&source=&warning=true";

            HttpResponseMessage response = await new HttpClient().GetAsync($"http://{InternalHost}:{InternalPort}/api/admin/log?{Parameters}");

            if (response.IsSuccessStatusCode)
            {
                var latestLogEntryString = await response.Content.ReadAsStringAsync();

                var latestLogEntry = JsonConvert.DeserializeObject<Log>(latestLogEntryString);

                if (latestLogEntry.LogEntries.FirstOrDefault() != null && !string.IsNullOrEmpty(latestLogEntry.LogEntries.First().Message))
                {
                    return latestLogEntry.LogEntries.First().Message;
                }
            }

            return String.Empty;
        }
    }
}
