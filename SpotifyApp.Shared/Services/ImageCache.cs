using Flurl.Http;
using SpotifyApp.Shared.Enums;

namespace SpotifyApp.Shared.Services;

internal class ImageCache : IImageCache 
{
    private const string CacheFolder = "Cache";
    
    public async Task<string> GetImage(string id, ImageType type, string webPath, CancellationToken cancellationToken)
    {
        if (!Directory.Exists(CacheFolder))
        {
            Directory.CreateDirectory(CacheFolder);
        }
        
        var fileName = $"{type}_{id}";

        var filePath = $"{CacheFolder}/{fileName}";
        if (File.Exists(filePath))
        {
            return filePath;
        }
        
        return await webPath
            .DownloadFileAsync(CacheFolder, fileName, cancellationToken: cancellationToken);
    }
}