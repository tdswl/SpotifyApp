using System.Collections.ObjectModel;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Api.Client.Tracks;
using SpotifyApp.Api.Contracts.Tracks.Requests;
using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.Services;
using SpotifyApp.Shared.ViewModels.Items;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class LikedSongsViewModel : ObservableRecipient
{
    private readonly IAuthService _authService;
    private readonly ITracksClient _tracksClient;
    private readonly IMapper _mapper;

    [ObservableProperty] 
    private PlaylistViewModel? _playlist;

    [ObservableProperty] 
    private ObservableCollection<TrackViewModel> _likedSongs = new();
    
    public LikedSongsViewModel()
    {
        //Designer constructor
    }
    
    public LikedSongsViewModel(IAuthService authService,
        ITracksClient tracksClient,
        IMapper mapper)
    {
        _authService = authService;
        _tracksClient = tracksClient;
        _mapper = mapper;

        GetTracksCommand.ExecuteAsync(null);
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetTracksAsync(CancellationToken token)
    {
        var currentItemsCount = LikedSongs.Count;
        var authInfo = await _authService.Login(token);
        var tracksInfoResponse = await _tracksClient.GetUsersSavedTracks(
            new GetUsersSavedTracksRequest{Offset = currentItemsCount,},
            authInfo.AccessToken,
            token);

        var playlistVm = Ioc.Default.GetRequiredService<PlaylistViewModel>();
        playlistVm.Item = new PlaylistModel
        {
            Id = Guid.NewGuid().ToString(),
            Images = new List<ImageModel>(),
            Name = "Liked Songs",
        };
        Playlist = playlistVm;
        
        for (var i = 0; i < tracksInfoResponse.Items.Count; i++)
        {
            var trackVm = Ioc.Default.GetRequiredService<TrackViewModel>();
            LikedSongs.Add(trackVm);
            var track = _mapper.Map<TrackModel>(tracksInfoResponse.Items[i].Track);
            track.Index = currentItemsCount + i + 1;
            trackVm.Item = track;
        }
    }
}