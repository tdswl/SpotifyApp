using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Services.Contracts;
using SpotifyApp.Services.Contracts.Models;

namespace SpotifyApp.Services;

internal sealed class ProfileService : IProfileService
{
    private readonly IImageCache _imageCache;
    private readonly ISpotifyClient _spotifyClient;

    public ProfileService(IImageCache imageCache,
        ISpotifyClient spotifyClient)
    {
        _imageCache = imageCache;
        _spotifyClient = spotifyClient;
    }

    async Task<ProfileModel> IProfileService.GetCurrentUserProfile(CancellationToken cancellationToken)
    {
        var currentSpotifyUser = await _spotifyClient.GetCurrentUsersProfileAsync(cancellationToken);

        var profileSpotifyImage = GetImage(currentSpotifyUser.Images);
        
        var cachedImageFilePath = await _imageCache.GetCachedImagePath(profileSpotifyImage.Url, cancellationToken);

        var currentUser = new ProfileModel
        {
            UserName = currentSpotifyUser.Display_name,
            ProfileImageUrl = cachedImageFilePath,
        };

        return currentUser;
    }

    private static ImageObject GetImage(ICollection<ImageObject> images)
    {
        return (images.Count switch
        {
            0 => null,
            1 => images.First(),
            _ => images.OrderBy(a => a.Width).ToList()[images.Count / 2]
        })!;
    }
}