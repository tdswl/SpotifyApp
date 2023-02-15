using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Services;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.ViewModels.Items;

public sealed class TrackViewModel : ImagePreviewViewModel
{
    public TrackViewModel(IImageCache imageCache) : base(imageCache)
    {
        PreviewSize = PreviewSize.Small;
    }
}