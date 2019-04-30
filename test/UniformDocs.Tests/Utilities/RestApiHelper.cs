using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UniformDocs.Tests.Utilities
{
    public static class RestApiHelper
    {
        private const int MaxRetries = 10;

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

        public static bool CheckAppRunning(string appName, ref int failsCount)
        {
            var numberOfAttempts = 0;

            using (HttpClient client = new HttpClient())
            {
                while (true)
                {
                    try
                    {
                        var response = client.GetAsync(
                            $"http://{Config.InternalHost}:{Config.InternalPort}/api/admin/databases/default/applications");

                        var httpResponseMessage = response.Result;
                        if (httpResponseMessage.IsSuccessStatusCode)
                        {
                            var runningAppsString = httpResponseMessage.Content.ReadAsStringAsync().Result;
                            var runningApps = JsonConvert.DeserializeObject<DatabaseApplicationsJson>(runningAppsString);

                            foreach (var app in runningApps.Items)
                            {
                                if (app.DisplayName == appName)
                                {
                                    return true;
                                }
                            }

                            return false;
                        }
                    }
                    catch (Exception)
                    {
                        numberOfAttempts++;

                        if (numberOfAttempts >= MaxRetries)
                        {
                            failsCount++;
                            throw;
                        }
                    }
                }
            }
        }

        public static async Task<string> GetLatestLogEntry()
        {
            string Parameters = "debug=false&error=true&maxitems=1&notice=false&source=&warning=true";

            HttpResponseMessage response = await new HttpClient().GetAsync($"http://{Config.InternalHost}:{Config.InternalPort}/api/admin/log?{Parameters}");

            if (response.IsSuccessStatusCode)
            {
                var latestLogEntryString = await response.Content.ReadAsStringAsync();

                var latestLogEntry = JsonConvert.DeserializeObject<Log>(latestLogEntryString);

                if (latestLogEntry.LogEntries.FirstOrDefault() != null && !string.IsNullOrEmpty(latestLogEntry.LogEntries.First().Message))
                {
                    return latestLogEntry.LogEntries.First().Message;
                }
            }

            return string.Empty;
        }
    }
}
