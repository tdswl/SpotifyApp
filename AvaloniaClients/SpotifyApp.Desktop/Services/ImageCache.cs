using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AsyncKeyedLock;
using SpotifyApp.Services.Contracts;

namespace SpotifyApp.Desktop.Services;

internal sealed class ImageCache : IImageCache 
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
    
    async Task<byte[]> IImageCache.GetCachedImagePath(string webPath, CancellationToken cancellationToken)
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
                return await File.ReadAllBytesAsync(filePath, cancellationToken);
            }

            await using (var webStream = await _httpClient.GetStreamAsync(webPath, cancellationToken))
            {
                await using (var fileStream = File.OpenWrite(filePath))
                {
                    await webStream.CopyToAsync(fileStream, cancellationToken);
                }
            }

            return await File.ReadAllBytesAsync(filePath, cancellationToken);
        }
    }
}