using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.Services;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.ViewModels.Items;

public sealed class TrackViewModel : ImagePreviewViewModel<TrackModel>
{
    public TrackViewModel(IImageCache imageCache) : base(imageCache)
    {
    }
}