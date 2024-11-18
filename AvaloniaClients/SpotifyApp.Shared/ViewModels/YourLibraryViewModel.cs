using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Messages;
using SpotifyApp.Shared.Models.NavigateParams;
using SpotifyApp.Shared.Properties;
using SpotifyApp.Shared.ViewModels.Base;
using SpotifyApp.Shared.ViewModels.SpotifyItems;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class YourLibraryViewModel : ViewModelWithInitialization
{
    private readonly ISpotifyClient _spotifyClient;
   
    [ObservableProperty] 
    private PlaylistViewModel? _selectedPlaylist;
    
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

    protected override Task Initialize(CancellationToken cancellationToken = default)
    {
        var likedSongsPlaylist = new PlaylistViewModel { Name = Resources.LikedSongs, Author = "0 songs", };
        Playlists.Add(likedSongsPlaylist);
        var loadLikedSongs = LoadLikedSongs(likedSongsPlaylist, cancellationToken);
        var loadPlaylists = LoadPlaylists(cancellationToken);

        return Task.WhenAll(loadLikedSongs, loadPlaylists);
    }

    protected override Task Deactivate(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    private async Task LoadPlaylists(CancellationToken cancellationToken)
    {
        var spotifyPlaylists = await _spotifyClient.GetAListOfCurrentUsersPlaylistsAsync(20, 0, cancellationToken);

        foreach (var spotifyPlaylist in spotifyPlaylists.Items)
        {
            Playlists.Add(new PlaylistViewModel(spotifyPlaylist));
        }
    }

    private async Task LoadLikedSongs(PlaylistViewModel playlistViewModel, CancellationToken cancellationToken)
    {
        var skip = 0;
        const int limit = 50;
        const string market = "TR";
        var savedTracks = new List<SavedTrackObject>();
        
        while (true)
        {
            var pagedSavedTracks = await _spotifyClient.GetUsersSavedTracksAsync(market, limit, skip, cancellationToken);
            savedTracks.AddRange(pagedSavedTracks.Items);
            playlistViewModel.Author = $"{savedTracks.Count} songs";
            
            // TODO: remove break. Only take first 50 for test purpose 
            break;

            if (pagedSavedTracks.Items.Count < limit)
            {
                break;
            }
            
            skip += limit;
        }
    }

    partial void OnSelectedPlaylistChanged(PlaylistViewModel? value)
    {
        if (value == null)
        {
            return;
        }
        
        WeakReferenceMessenger.Default.Send(new NavigateMessage
        {
            Type = PageType.PlaylistDetails,
            NavigateParams = new PlaylistParams { Id = value.Id, },
        });
    }
}