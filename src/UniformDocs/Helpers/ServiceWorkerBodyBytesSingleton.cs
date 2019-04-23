using Starcounter;
using System;
using System.Text;

namespace UniformDocs
{
    public sealed class ServiceWorkerBodyBytesSingleton
    {
        private static Encoding _defaultEncoding = Encoding.UTF8;
        private string _sourceTemplate;
        private byte[] _hydrated;

        private ServiceWorkerBodyBytesSingleton()
        {
            _regenerateKeyAndHydrateTemplate();
            Starcounter.Internal.AppsBootstrapper.WatchResources(_resourceChanged);
        }
       
        private static string FetchServiceWorkerTemplate()
        {
            string sourceUrl = "/sys/app-shell/service-worker-source.js";
            try
            {
                return Self.GET(sourceUrl).Body;
            }
            catch(Exception e)
            {
                var err = e;
                throw new Exception($"Could not fetch {sourceUrl}");
            }
        }

        private static readonly Lazy<ServiceWorkerBodyBytesSingleton> s_lazy = new Lazy<ServiceWorkerBodyBytesSingleton>(() => new ServiceWorkerBodyBytesSingleton());

        public static ServiceWorkerBodyBytesSingleton Instance => s_lazy.Value;

        private void _regenerateKeyAndHydrateTemplate()
        {
            string key = Guid.NewGuid().ToString();
            _rehydrateTemplate(key);
        }

        private void _rehydrateTemplate(string key)
        {
            if (string.IsNullOrEmpty(_sourceTemplate))
            {
                _sourceTemplate = FetchServiceWorkerTemplate();
            }
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
        /// <param name="_resourceChanged"></param>
        private void _resourceChanged(string newUri, string oldUri)
        {
            /* FIXME: currently this is throwing "You are trying to perform a Self call while not being on Starcounter scheduler" 
            
            // the service worker source file has changed. Re-fetch the source template
            // this cancels the need for restarting the app(s) to get the effects of updating the service-worker-source template
            if(newUri.EndsWith("service-worker-source.js"))
            {
                _serviceWorkerTemplate = FetchServiceWorkerTemplate();
            }
            */
            _regenerateKeyAndHydrateTemplate();
        }
    }
}