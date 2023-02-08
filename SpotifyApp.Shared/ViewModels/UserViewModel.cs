using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Api.Contracts.Users.Responses;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Services;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class UserViewModel : ObservableRecipient
{
    private readonly IImageCache _imageCache;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GetProfileImageCommand))]
    private GetCurrentUserProfileResponse? _userProfile;

    [ObservableProperty]
    private Bitmap? _avatar;

    public UserViewModel(IImageCache imageCache,
        GetCurrentUserProfileResponse userProfile)
    {
        _imageCache = imageCache;
        UserProfile = userProfile;
    }

    partial void OnUserProfileChanged(GetCurrentUserProfileResponse? value)
    {
        if (value != null)
        {
            GetProfileImageCommand.ExecuteAsync(null);
        }
    }

    [RelayCommand(IncludeCancelCommand = true, CanExecute = nameof(GetProfileImageCanExecute))]
    private async Task GetProfileImageAsync(CancellationToken token)
    {
        var avatar = UserProfile?.Images.FirstOrDefault();
        if (avatar != null)
        {
            var imagePath = await _imageCache.GetImage(UserProfile.Id,
                ImageType.Profile,
                avatar.Url,
                token);

            await using (var fileStream = File.OpenRead(imagePath))
            {
                Avatar = await Task.Run(() => Bitmap.DecodeToWidth(fileStream, 400), token);
            }
        }
    }

    private bool GetProfileImageCanExecute()
    {
        return UserProfile != null;
    }
}