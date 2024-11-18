using CommunityToolkit.Mvvm.ComponentModel;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class ProfileViewModel : ViewModelWithInitialization
{
    private readonly ISpotifyClient _spotifyClient;

    [ObservableProperty] 
    private string? _userName;
    
    [ObservableProperty] 
    private ImageViewModel _image;

    public ProfileViewModel()
    {
        // Designer constructor
    }
    
    public ProfileViewModel(ISpotifyClient spotifyClient)
    {
        _spotifyClient = spotifyClient;
    }

    protected override async Task Initialize(CancellationToken cancellationToken = default)
    {
        var profile = await _spotifyClient.GetCurrentUsersProfileAsync(cancellationToken);
        
        UserName = profile.Display_name;
        Image = new ImageViewModel(profile.Images);
    }

    protected override Task Deactivate(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}