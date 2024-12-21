using System.Runtime.InteropServices.JavaScript;
using System.Threading;
using System.Threading.Tasks;
using AsyncKeyedLock;
using SpotifyApp.Services.Contracts;

namespace SpotifyApp.Browser.Services;

internal sealed class ImageCache : IImageCache 
{
    private static readonly AsyncKeyedLocker<string> AsyncKeyedLocker = new(o =>
    {
        o.PoolSize = 20;
        o.PoolInitialFill = 1;
    });
    
    async Task<byte[]> IImageCache.GetCachedImagePath(string webPath, CancellationToken cancellationToken)
    {
        using (await AsyncKeyedLocker.LockAsync(webPath, cancellationToken))
        {
            using (await JSHost.ImportAsync("cacheStorage.js", "../cacheStorage.js", cancellationToken))
            {
                return Interop.CacheStorageInterop.CacheData(webPath);
            }
        }
    }
}