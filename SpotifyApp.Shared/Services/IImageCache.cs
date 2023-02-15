namespace SpotifyApp.Shared.Services;

public interface IImageCache
{
    Task<string> GetCachedImagePath(string webPath,
        CancellationToken cancellationToken);
}