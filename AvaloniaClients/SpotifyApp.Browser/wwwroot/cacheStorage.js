// Try to get data from the cache, but fall back to fetching it live.
export async function cacheData(url) {
    const cacheVersion = 1;
    const cacheName = `spotify-app-183C8380-${cacheVersion}`;
    let cachedData = await getCachedData(cacheName, url);
    if (cachedData) {
        console.log("Retrieved cached data");
        return cachedData;
    }

    console.log("Fetching fresh data");

    const cacheStorage = await caches.open(cacheName);
    await cacheStorage.add(url);
    cachedData = await getCachedData(cacheName, url);
    await deleteOldCaches(cacheName);

    return cachedData;
}

// Get data from the cache.
export async function getCachedData(cacheName, url) {
    const cacheStorage = await caches.open(cacheName);
    const cachedResponse = await cacheStorage.match(url);

    if (!cachedResponse || !cachedResponse.ok) {
        return false;
    }

    return await cachedResponse.json();
}

// Delete any old caches to respect user's disk space.
export async function deleteOldCaches(currentCache) {
    const keys = await caches.keys();

    for (const key of keys) {
        const isOurCache = key.startsWith("spotify-app-183C8380-");
        if (currentCache === key || !isOurCache) {
            continue;
        }
        await caches.delete(key);
    }
}
