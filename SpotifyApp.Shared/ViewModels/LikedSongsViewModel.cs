using System.Collections.ObjectModel;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.Properties;
using SpotifyApp.Shared.ViewModels.Items;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class LikedSongsViewModel : ObservableRecipient
{
    private readonly ISpotifyClient _spotifyClient;
    private readonly IMapper _mapper;

    [ObservableProperty] 
    private PlaylistViewModel? _playlist;

    [ObservableProperty] 
    private ObservableCollection<TrackViewModel> _likedSongs = new();
    
    public LikedSongsViewModel()
    {
        //Designer constructor
    }
    
    public LikedSongsViewModel(ISpotifyClient spotifyClient,
        IMapper mapper)
    {
        _spotifyClient = spotifyClient;
        _mapper = mapper;
        IsActive = true;
    }

    protected override void OnActivated()
    {
        base.OnActivated();
        
        GetTracksCommand.ExecuteAsync(null);
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetTracksAsync(CancellationToken token)
    {
        var currentItemsCount = LikedSongs.Count;
        var savedTracks = await _spotifyClient.GetUsersSavedTracksAsync(null, null, currentItemsCount, token);
        var playlistVm = Ioc.Default.GetRequiredService<PlaylistViewModel>();
        playlistVm.Item = new PlaylistModel
        {
            Id = Guid.NewGuid().ToString(),
            Images = new List<ImageModel>(),
            Name = Resources.LikedSongs,
        };
        
        if (token.IsCancellationRequested)
        {
            return;
        }
        
        Playlist = playlistVm;

        foreach (var savedTrack in savedTracks.Items)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }
            
            var trackVm = Ioc.Default.GetRequiredService<TrackViewModel>();
            LikedSongs.Add(trackVm);
            var track = _mapper.Map<TrackModel>(savedTrack.Track);
            track.Index = ++currentItemsCount;
            trackVm.Item = track;
        }
    }

    protected override void OnDeactivated()
    {
        GetTracksCommand.Cancel();
        Playlist?.Dispose();
        Playlist = null;
        foreach (var song in LikedSongs)
        {
            song.Dispose();
        }
        LikedSongs.Clear();
    }
}