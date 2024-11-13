using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using SpotifyApp.Services.Contracts;
using SpotifyApp.Shared.Helpers;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class ProfileViewModel : Base.ViewModelWithInitialization
{
    private readonly IProfileService _profileService;

    [ObservableProperty] 
    private string? _userName;
    
    [ObservableProperty] 
    private Bitmap? _image;

    public ProfileViewModel()
    {
        // Designer constructor
    }
    
    public ProfileViewModel(IProfileService profileService)
    {
        _profileService = profileService;
    }

    protected override async Task Initialize(CancellationToken cancellationToken = default)
    {
        var profile = await _profileService.GetCurrentUserProfile(cancellationToken);
        
        UserName = profile.UserName;
        if (profile.ProfileImageUrl != null)
        {
            Image = ImageHelper.LoadFromFile(profile.ProfileImageUrl);
        }
    }

    protected override Task Deactivate(CancellationToken cancellationToken = default)
    {
        Image?.Dispose();
        return Task.CompletedTask;
    }
}