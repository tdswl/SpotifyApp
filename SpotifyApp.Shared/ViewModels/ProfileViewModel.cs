using System.Collections.ObjectModel;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Api.Client.Tracks;
using SpotifyApp.Api.Client.Users;
using SpotifyApp.Api.Contracts.Base.Enums;
using SpotifyApp.Api.Contracts.Tracks.Requests;
using SpotifyApp.Api.Contracts.Users.Requests;
using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.Services;
using SpotifyApp.Shared.ViewModels.Items;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class ProfileViewModel : ObservableRecipient
{
    private readonly IAuthService _authService;
    private readonly IUsersClient _usersClient;
    private readonly ITracksClient _tracksClient;
    private readonly IMapper _mapper;
    
    [ObservableProperty]
    private UserViewModel? _currentUser;

    [ObservableProperty] 
    private ObservableCollection<ArtistViewModel> _topArtists = new();
    
    [ObservableProperty] 
    private ObservableCollection<TrackViewModel> _topTracks = new();
    
    [ObservableProperty] 
    private ObservableCollection<ArtistViewModel> _followingArtists = new();
    
    public ProfileViewModel()
    {
        //Designer constructor
    }
    
    public ProfileViewModel(IAuthService authService,
        IUsersClient usersClient,
        ITracksClient tracksClient,
        IMapper mapper)
    {
        _authService = authService;
        _usersClient = usersClient;
        _tracksClient = tracksClient;
        _mapper = mapper;

        GetUserInfoCommand.ExecuteAsync(null);
        GetArtistsCommand.ExecuteAsync(null);
        GetTracksCommand.ExecuteAsync(null);
        GetFollowingArtistsCommand.ExecuteAsync(null);
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetUserInfoAsync(CancellationToken token)
    {
        var authInfo = await _authService.Login(token);
        var userInfo = await _usersClient.GetCurrentUserProfile(authInfo.AccessToken, token);
        var userVm = Ioc.Default.GetRequiredService<UserViewModel>();
        userVm.Item = _mapper.Map<UserModel>(userInfo);
        CurrentUser = userVm;
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetArtistsAsync(CancellationToken token)
    {
        var authInfo = await _authService.Login(token);
        var artistsResponse = await _usersClient.GetUsersTopItems(
            new GetUsersTopItemsRequest { Type = ItemsTypeApi.Artist, },
            authInfo.AccessToken,
            token);

        TopArtists.Clear();
        foreach (var artist in artistsResponse.Items)
        {
            var artistVm = Ioc.Default.GetRequiredService<ArtistViewModel>();
            TopArtists.Add(artistVm);
            artistVm.Item = _mapper.Map<ArtistModel>(artist);
        }
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetTracksAsync(CancellationToken token)
    {
        var authInfo = await _authService.Login(token);
        var topTracksResponse = await _usersClient.GetUsersTopItems(
            new GetUsersTopItemsRequest { Type = ItemsTypeApi.Track, Limit = 4, },
            authInfo.AccessToken,
            token);

        // 50 - max by api
        var trackIds = string.Join(",", topTracksResponse.Items.Take(50).Select(a => a.Id));
        var tracksInfoResponse = await _tracksClient.GetSeveralTracks(
            new GetSeveralTracksRequest { Ids = trackIds, },
            authInfo.AccessToken,
            token);

        TopTracks.Clear();
        for (var i = 0; i < tracksInfoResponse.Tracks.Count; i++)
        {
            var trackVm = Ioc.Default.GetRequiredService<TrackViewModel>();
            TopTracks.Add(trackVm);
            var track = _mapper.Map<TrackModel>(tracksInfoResponse.Tracks[i]);
            track.Index = i + 1;
            trackVm.Item = track;
        }
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetFollowingArtistsAsync(CancellationToken token)
    {
        var authInfo = await _authService.Login(token);
        var artistsResponse = await _usersClient.GetFollowedArtists(
            new GetFollowedArtistsRequest { Type = ItemsTypeApi.Artist, },
            authInfo.AccessToken,
            token);

        FollowingArtists.Clear();
        foreach (var artist in artistsResponse.Artists.Items)
        {
            var artistVm = Ioc.Default.GetRequiredService<ArtistViewModel>();
            FollowingArtists.Add(artistVm);
            artistVm.Item = _mapper.Map<ArtistModel>(artist);
        }
    }
}