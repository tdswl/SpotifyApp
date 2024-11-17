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
    private string? _webImageUrl;

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

        var image = GetImage(profile.Images);
        if (image != null)
        {
            WebImageUrl = image.Url;
        }
    }

    protected override Task Deactivate(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
    
    private static ImageObject? GetImage(ICollection<ImageObject> images)
    {
        return (images.Count switch
        {
            0 => null,
            1 => images.First(),
            _ => images.OrderBy(a => a.Width).ToList()[images.Count / 2]
        })!;
    }
}