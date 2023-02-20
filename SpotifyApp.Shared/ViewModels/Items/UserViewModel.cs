using SpotifyApp.Shared.Services;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.ViewModels.Items;

public sealed class UserViewModel : ImagePreviewViewModel
{
    public UserViewModel(IImageCache imageCache) : base(imageCache)
    {
        IsActive = true;
    }
}