using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Shared.ViewModels.Base;
using SpotifyApp.Shared.ViewModels.SpotifyItems;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class YourLibraryViewModel : ViewModelWithInitialization
{
    private readonly ISpotifyClient _spotifyClient;
   
    [ObservableProperty] 
    private ObservableCollection<PlaylistViewModel> _playlists = [];

    public YourLibraryViewModel()
    {
        // Designer constructor
    }
    
    public YourLibraryViewModel(ISpotifyClient spotifyClient)
    {
        _spotifyClient = spotifyClient;
    }

    protected override async Task Initialize(CancellationToken cancellationToken = default)
    {
        var skip = 0;
        const int limit = 50;
        const string market = "TR";
        var savedTracks = new List<SavedTrackObject>();
        
        while (true)
        {
            var pagedSavedTracks = await _spotifyClient.GetUsersSavedTracksAsync(market, limit, skip, cancellationToken);
            savedTracks.AddRange(pagedSavedTracks.Items);

            if (pagedSavedTracks.Items.Count < limit)
            {
                break;
            }
            
            skip += limit;
        }
        Playlists.Add(new PlaylistViewModel(savedTracks));
        
        var spotifyPlaylists = await _spotifyClient.GetAListOfCurrentUsersPlaylistsAsync(20, 0, cancellationToken);

        foreach (var spotifyPlaylist in spotifyPlaylists.Items)
        {
            Playlists.Add(new PlaylistViewModel(spotifyPlaylist));
        }
    }

    protected override Task Deactivate(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}