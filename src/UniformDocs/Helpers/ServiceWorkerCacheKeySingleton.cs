using System;

namespace UniformDocs
{
    public sealed class ServiceWorkerCacheKeySingleton
    {
        private ServiceWorkerCacheKeySingleton()
        {
            RegenerateKey();
            Starcounter.Internal.AppsBootstrapper.WatchResources(ResourceChanged);
        }

        private static readonly Lazy<ServiceWorkerCacheKeySingleton> lazy = new Lazy<ServiceWorkerCacheKeySingleton>(() => new ServiceWorkerCacheKeySingleton());

        public static ServiceWorkerCacheKeySingleton Instance => lazy.Value;

        private string Key;

        private void RegenerateKey()
        {
            Key = Guid.NewGuid().ToString();
        }

        public string GetKey()
        {
            return Key;
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
            RegenerateKey();
        }
    }
}