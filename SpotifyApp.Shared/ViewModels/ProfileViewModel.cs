using System.Collections.ObjectModel;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Api.Client.Users;
using SpotifyApp.Api.Contracts.Users.Enums;
using SpotifyApp.Api.Contracts.Users.Requests;
using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.Services;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class ProfileViewModel : ObservableRecipient
{
    private readonly IAuthService _authService;
    private readonly IUsersClient _usersClient;
    private readonly IImageCache _imageCache;
    private readonly IMapper _mapper;
    
    [ObservableProperty]
    private UserViewModel? _currentUser;

    [ObservableProperty] 
    private ObservableCollection<AlbumViewModel> _topArtists = new();
    
    [ObservableProperty] 
    private ObservableCollection<TrackViewModel> _topTracks = new();
    
    public ProfileViewModel()
    {
        //Designer constructor
    }
    
    public ProfileViewModel(IAuthService authService,
        IUsersClient usersClient,
        IImageCache imageCache,
        IMapper mapper)
    {
        _authService = authService;
        _usersClient = usersClient;
        _imageCache = imageCache;
        _mapper = mapper;

        GetUserInfoCommand.Execute(null);
        GetArtistsCommand.Execute(null);
        GetTracksCommand.Execute(null);
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetUserInfoAsync(CancellationToken token)
    {
        var authInfo = await _authService.Login(token);
        var userInfo = await _usersClient.GetCurrentUserProfile(authInfo.AccessToken, token);
        CurrentUser = new UserViewModel(_imageCache)
        {
            Item = _mapper.Map<UserModel>(userInfo),
        };
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetArtistsAsync(CancellationToken token)
    {
        var authInfo = await _authService.Login(token);
        var artistsResponse = await _usersClient.GetUsersTopItems(
            new GetUsersTopItemsRequest { Type = ItemsTypeApi.Artist },
            authInfo.AccessToken,
            token);

        TopArtists.Clear();
        foreach (var artist in artistsResponse.Items)
        {
            TopArtists.Add(new AlbumViewModel(_imageCache)
            {
                Item = _mapper.Map<ItemModel>(artist),
            });
        }
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetTracksAsync(CancellationToken token)
    {
        var authInfo = await _authService.Login(token);
        var tracksResponse = await _usersClient.GetUsersTopItems(
            new GetUsersTopItemsRequest { Type = ItemsTypeApi.Track },
            authInfo.AccessToken,
            token);

        TopTracks.Clear();
        foreach (var track in tracksResponse.Items)
        {
            TopTracks.Add(new TrackViewModel(_imageCache)
            {
                Item = _mapper.Map<ItemModel>(track),
            });
        }
    }
}