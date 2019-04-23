const version = 'REPLACE_ME_WTH_RUNTIME_HASH';
/**
 * This function's logic determins `url` is cached in the service worker cache. 
 * Feel free to extend is logic with whatever critera you think is needed for caching urls
 * @param {String} url 
 * @returns {Boolean} whether to cache the URL or not.
 */
function shouldCacheThisURL(url) {
  // get the last part of the url after the last `/`;
  const lastPartOfUrl = url.split('/').pop();

  /* if the last part matches ends with an extension that is (2-5) in length
     it's probably a file (eg: file.jpg), worth caching
   */
  if (lastPartOfUrl.match(/\.\w{2,5}$/)) {
    // has an extenstion
    return true;
  }
  /**
   * If the url has `htmlmerger` word in it, then it's a dynamic yet cachable URL. Since the url's uniqueness
   * is tightly coupled with the content's uniqueness in this case
   */
  const urlInstance = new URL(url);
  if (urlInstance.pathname === '/sc/htmlmerger') {
    // an HTML merger url
    return true;
  }
  /**
   * The url probably represents a JSON model that shouldn't be cached
   */
  return false;
}

self.addEventListener('install', function(event) {
  console.log('[ServiceWorker] Installed version', version);
  console.log('[ServiceWorker] Skip waiting on install');
  return self.skipWaiting();
});

self.addEventListener('activate', function(event) {
  self.clients
    .matchAll({
      includeUncontrolled: true
    })
    .then(function(clientList) {
      var urls = clientList.map(function(client) {
        return client.url;
      });
      console.log('[ServiceWorker] Matching clients:', urls.join(', '));
    });

  event.waitUntil(
    caches
      .keys()
      .then(function(cacheNames) {
        return Promise.all(
          cacheNames.map(function(cacheName) {
            if (cacheName !== version) {
              console.log('[ServiceWorker] Deleting old cache:', cacheName);
              return caches.delete(cacheName);
            }
          })
        );
      })
      .then(function() {
        console.log('[ServiceWorker] Claiming clients for version', version);
        return self.clients.claim();
      })
  );
});

// The fetch handler serves responses for same-origin resources from a cache.
// If no response is found, it populates the runtime cache with the response
// from the network before returning it to the page.
self.addEventListener('fetch', event => {
  // Skip cross-origin requests, like those for Google Analytics.
  if (event.request.url.startsWith(self.location.origin)) {
    const acceptHeader = event.request.headers.get('accept');

    // skip Palindrom objects
    if (acceptHeader && acceptHeader.includes('application/json')) {
      return;
    }

    event.respondWith(
      caches.match(event.request).then(cachedResponse => {
        if (cachedResponse) {
          return cachedResponse;
        }
        return caches.open(version).then(cache => {
          return fetch(event.request).then(response => {
            // only cache assets
            // caching non-assets would cache eg MainPage (which is technically the app shell),
            // and would cache REST API calls
            if (shouldCacheThisURL(event.request.url)) {
              return cache.put(event.request, response.clone()).then(() => {
                return response;
              });
            } else {
              return response;
            }
          });
        });
      })
    );
  }
});
