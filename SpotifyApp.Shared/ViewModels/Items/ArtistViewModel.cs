using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.Services;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.ViewModels.Items;

public sealed class ArtistViewModel : ImagePreviewViewModel<ArtistModel>
{
    public ArtistViewModel(IImageCache imageCache) : base(imageCache)
    {
    }
}