using AsyncKeyedLock;

namespace SpotifyApp.Shared.Services;

internal class ImageCache : IImageCache 
{
    private const string CacheFolder = "Cache";
    private readonly HttpClient _httpClient;
    private static readonly AsyncKeyedLocker<string> AsyncKeyedLocker = new(o =>
    {
        o.PoolSize = 20;
        o.PoolInitialFill = 1;
    });

    public ImageCache(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }
    
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

            await using (var webStream = await _httpClient.GetStreamAsync(webPath, cancellationToken))
            {
                await using (var fileStream = File.OpenWrite(filePath))
                {
                    await webStream.CopyToAsync(fileStream, cancellationToken);
                }
            }

            return filePath;
        }
    }
}