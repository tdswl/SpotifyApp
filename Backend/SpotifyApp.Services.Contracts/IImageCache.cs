namespace SpotifyApp.Services.Contracts;

public interface IImageCache
{
    Task<byte[]> GetCachedImagePath(string webPath, CancellationToken cancellationToken);
}