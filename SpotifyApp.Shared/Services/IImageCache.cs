using SpotifyApp.Shared.Enums;

namespace SpotifyApp.Shared.Services;

public interface IImageCache
{
    Task<string> GetImage(string id,
        ImageType type,
        string webPath,
        CancellationToken cancellationToken);
}