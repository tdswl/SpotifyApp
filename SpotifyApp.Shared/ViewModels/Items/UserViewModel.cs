using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.Services;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.ViewModels.Items;

public sealed class UserViewModel : ImagePreviewViewModel<UserModel>
{
    public UserViewModel(IImageCache imageCache) : base(imageCache)
    {
    }
}