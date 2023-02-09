using System.Collections.ObjectModel;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Api.Client.Users;
using SpotifyApp.Api.Contracts.Users.Enums;
using SpotifyApp.Api.Contracts.Users.Requests;
using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.Services;
using SpotifyApp.Shared.ViewModels.Items;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class ProfileViewModel : ObservableRecipient
{
    private readonly IAuthService _authService;
    private readonly IUsersClient _usersClient;
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
        IMapper mapper)
    {
        _authService = authService;
        _usersClient = usersClient;
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
            artistVm.Item = _mapper.Map<ItemModel>(artist);
        }
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetTracksAsync(CancellationToken token)
    {
        var authInfo = await _authService.Login(token);
        var tracksResponse = await _usersClient.GetUsersTopItems(
            new GetUsersTopItemsRequest { Type = ItemsTypeApi.Track, Limit = 4, },
            authInfo.AccessToken,
            token);

        TopTracks.Clear();
        foreach (var track in tracksResponse.Items)
        {
            var trackVm = Ioc.Default.GetRequiredService<TrackViewModel>();
            TopTracks.Add(trackVm);
            trackVm.Item = _mapper.Map<ItemModel>(track);
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
            artistVm.Item = _mapper.Map<ItemModel>(artist);
        }
    }
}