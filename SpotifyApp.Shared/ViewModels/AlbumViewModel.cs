using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.Services;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.ViewModels;

public sealed class AlbumViewModel : ImagePreviewViewModel<ItemModel>
{
    public AlbumViewModel(IImageCache imageCache) : base(imageCache)
    {
    }
}