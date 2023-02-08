namespace SpotifyApp.Shared.Services;

public interface IImageCache
{
    Task<string> GetImage(string webPath,
        CancellationToken cancellationToken);
}