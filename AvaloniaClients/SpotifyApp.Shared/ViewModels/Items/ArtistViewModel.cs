using SpotifyApp.Services.Contracts;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.ViewModels.Items;

public sealed class ArtistViewModel : ImagePreviewViewModel
{
    public ArtistViewModel(IImageCache imageCache) : base(imageCache)
    {
        IsActive = true;
    }
}