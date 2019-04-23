using Starcounter;
using System;
using System.Text;

namespace UniformDocs
{
    public sealed class ServiceWorkerBodyBytesSingleton
    {
        private static Encoding defaultEncoding = Encoding.UTF8;
        private string serviceWorkerTemplate;
        private byte[] CachedHydratedServiceWorkerBytes;

        private ServiceWorkerBodyBytesSingleton()
        {
            RegenerateKeyAndHydrateServiceWorkerTemplate();
            Starcounter.Internal.AppsBootstrapper.WatchResources(ResourceChanged);
        }
       
        private static string FetchServiceWorkerTemplate()
        {
            try
            {
                string ServiceWorkerUrl = "/sys/service-worker-source.js";
                return Self.GET(ServiceWorkerUrl).Body;
            }
            catch
            {
                throw new Exception(@"Could not fetch /sys/service-worker-source.js");
            }
        }

        private static readonly Lazy<ServiceWorkerBodyBytesSingleton> lazy = new Lazy<ServiceWorkerBodyBytesSingleton>(() => new ServiceWorkerBodyBytesSingleton());

        public static ServiceWorkerBodyBytesSingleton Instance => lazy.Value;

        private void RegenerateKeyAndHydrateServiceWorkerTemplate()
        {
            string Key = Guid.NewGuid().ToString();
            RehydrateServiceWorkerTemplate(Key);
        }

        private void RehydrateServiceWorkerTemplate(string key)
        {
            string template;
            if (!string.IsNullOrEmpty(serviceWorkerTemplate))
            {
                template = serviceWorkerTemplate;
            }
            else
            {
                template = FetchServiceWorkerTemplate();
                serviceWorkerTemplate = template;
            }
            var SWBody = template.Replace("REPLACE_ME_WTH_RUNTIME_HASH", key);
            CachedHydratedServiceWorkerBytes = defaultEncoding.GetBytes(SWBody);
        }
        /// <summary>
        /// Returns the byte-array of UTF8 encoded service-worker JavaScript code
        /// </summary>
        public byte[] GetServiceWorkerBodyBytes()
        {
            return CachedHydratedServiceWorkerBytes;
        }

        /// <summary>
        /// in case of update, oldUri == newUri
        /// in case of rename, oldUri != newUri 
        /// in case of delete, newUri is null
        /// in case of a new file, oldUri is null
        /// </summary>
        /// <param name="resourceChanged"></param>
        private void ResourceChanged(string newUri, string oldUri)
        {
            RegenerateKeyAndHydrateServiceWorkerTemplate();
        }
    }
}