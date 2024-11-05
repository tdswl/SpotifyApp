using SpotifyApp.Shared.Services;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.ViewModels.Items;

public sealed class AlbumViewModel : ImagePreviewViewModel
{
    public AlbumViewModel(IImageCache imageCache) : base(imageCache)
    {
        IsActive = true;
    }
}