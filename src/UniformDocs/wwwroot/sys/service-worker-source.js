const RUNTIME = 'REPLACE_ME_WTH_RUNTIME_HASH';

function isRequestForAnAsset(url) {
  const lastPartOfUrl = url.split('/').pop();
  if (lastPartOfUrl.match(/\.\w{2,5}$/)) {
    // has an extenstion
    return true;
  }
  if (url.includes('htmlmerger')) {
    // an HTML merger url
    return true;
  }
  return false;
}

// The activate handler takes care of cleaning up old caches.
self.addEventListener('activate', event => {
  const currentCaches = [RUNTIME];
  event.waitUntil(
    caches
      .keys()
      .then(cacheNames => {
        return cacheNames.filter(
          cacheName => !currentCaches.includes(cacheName)
        );
      })
      .then(cachesToDelete => {
        return Promise.all(
          cachesToDelete.map(cacheToDelete => {
            return caches.delete(cacheToDelete);
          })
        );
      })
      .then(() => self.clients.claim())
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
        return caches.open(RUNTIME).then(cache => {
          return fetch(event.request).then(response => {
            // only cache assets
            // caching non-assets would cache eg MainPage (which is technically the app shell),
            // and would cache REST API calls
            if (isRequestForAnAsset(event.request.url)) {
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
