using AsyncKeyedLock;
using Flurl.Http;

namespace SpotifyApp.Shared.Services;

internal class ImageCache : IImageCache 
{
    private const string CacheFolder = "Cache";
    private static readonly AsyncKeyedLocker<string> AsyncKeyedLocker = new(o =>
    {
        o.PoolSize = 20;
        o.PoolInitialFill = 1;
    });
    
    public async Task<string> GetCachedImagePath(string webPath, CancellationToken cancellationToken)
    {
        using (await AsyncKeyedLocker.LockAsync(webPath, cancellationToken))
        {
            if (!Directory.Exists(CacheFolder))
            {
                Directory.CreateDirectory(CacheFolder);
            }

            var fileName = Path.GetFileName(new Uri(webPath).AbsolutePath);

            var filePath = $"{CacheFolder}/{fileName}";
            if (File.Exists(filePath))
            {
                return filePath;
            }

            return await webPath
                .DownloadFileAsync(CacheFolder, fileName, cancellationToken: cancellationToken);
        }
    }
}