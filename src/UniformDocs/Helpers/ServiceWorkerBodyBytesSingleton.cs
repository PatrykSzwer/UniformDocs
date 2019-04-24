using Starcounter;
using System;
using System.Text;
using System.Threading;

namespace UniformDocs
{
    public sealed class ServiceWorkerBodyBytesSingleton
    {
        private static Encoding _defaultEncoding = Encoding.UTF8;
        private string _sourceTemplate;
        private byte[] _hydrated;

        private ServiceWorkerBodyBytesSingleton()
        {
            RegenerateKeyAndHydrateTemplate();
            Starcounter.Internal.AppsBootstrapper.WatchResources(ResourceChanged);
        }

        private static string FetchServiceWorkerTemplate()
        {
            string sourceUrl = "/sys/app-shell/service-worker-source.js";
            try
            {
                return Self.GET(sourceUrl).Body;
            }
            catch
            {
                throw new Exception($"Could not fetch {sourceUrl}");
            }
        }

        private static readonly Lazy<ServiceWorkerBodyBytesSingleton> s_lazy = new Lazy<ServiceWorkerBodyBytesSingleton>(() => new ServiceWorkerBodyBytesSingleton());

        public static ServiceWorkerBodyBytesSingleton Instance => s_lazy.Value;

        private void RegenerateKeyAndHydrateTemplate()
        {
            string key = Guid.NewGuid().ToString();
            RehydrateTemplate(key);
        }

        private void RehydrateTemplate(string key)
        {
            _sourceTemplate = _sourceTemplate ?? FetchServiceWorkerTemplate();
            string body = _sourceTemplate.Replace("REPLACE_ME_WTH_RUNTIME_HASH", key);
            _hydrated = _defaultEncoding.GetBytes(body);
        }
        /// <summary>
        /// Returns the byte-array of UTF8 encoded service-worker JavaScript code
        /// </summary>
        public byte[] GetBodyBytes()
        {
            return _hydrated;
        }

        /// <summary>
        /// in case of update, oldUri == newUri
        /// in case of rename, oldUri != newUri 
        /// in case of delete, newUri is null
        /// in case of a new file, oldUri is null
        /// </summary>
        /// <param name="ResourceChanged"></param>
        async private void ResourceChanged(string newUri, string oldUri)
        {
            // the service worker source file has changed. Re-fetch the source template
            // this cancels the need for restarting the app(s) to get the effects of updating the service-worker-source template
            if (newUri.EndsWith("service-worker-source.js"))
            {
                await Scheduling.RunTask(() => _sourceTemplate = FetchServiceWorkerTemplate());
                RegenerateKeyAndHydrateTemplate();
            }
            else
            {
                RegenerateKeyAndHydrateTemplate();
            }
        }
    }
}