namespace SpotifyApp.Services.Contracts;

public interface IImageCache
{
    Task<string> GetCachedImagePath(string webPath, CancellationToken cancellationToken);
}