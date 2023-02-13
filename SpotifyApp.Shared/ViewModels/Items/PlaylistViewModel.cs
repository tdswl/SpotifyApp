using SpotifyApp.Shared.Services;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.ViewModels.Items;

public sealed class PlaylistViewModel : ImagePreviewViewModel
{
    public PlaylistViewModel(IImageCache imageCache) : base(imageCache)
    {
    }
}