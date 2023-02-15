using System.Collections.ObjectModel;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SpotifyApp.Api.Client.Tracks;
using SpotifyApp.Api.Client.Users;
using SpotifyApp.Api.Contracts.Base.Enums;
using SpotifyApp.Api.Contracts.Tracks.Requests;
using SpotifyApp.Api.Contracts.Users.Requests;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Messages;
using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.Models.NavigateParams;
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
    private ArtistViewModel? _selectedTopArtist;
    
    [ObservableProperty] 
    private ArtistViewModel? _selectedFollowingArtist;

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
        IsActive = true;
    }

    protected override void OnActivated()
    {
        base.OnActivated();

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
        
        if (token.IsCancellationRequested)
        {
            return;
        }
        
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
            if (token.IsCancellationRequested)
            {
                return;
            }
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
            if (token.IsCancellationRequested)
            {
                return;
            }
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
            if (token.IsCancellationRequested)
            {
                return;
            }
            var artistVm = Ioc.Default.GetRequiredService<ArtistViewModel>();
            FollowingArtists.Add(artistVm);
            artistVm.Item = _mapper.Map<ArtistModel>(artist);
        }
    }

    [RelayCommand]
    private void OpenArtist(string id)
    {
        WeakReferenceMessenger.Default.Send(new NavigateMessage
        {
            Type = PageType.Artist,
            NavigateParams = new ArtistParams { Id = id },
        });
    }
    
    partial void OnSelectedTopArtistChanged(ArtistViewModel? value)
    {
        if (value != null)
        {
            OpenArtistCommand.Execute(value.Item.Id);
        }
    }
    
    partial void OnSelectedFollowingArtistChanged(ArtistViewModel? value)
    {
        if (value != null)
        {
            OpenArtistCommand.Execute(value.Item.Id);
        }
    }

    protected override void OnDeactivated()
    {
        GetUserInfoCommand.Cancel();
        GetArtistsCommand.Cancel();
        GetTracksCommand.Cancel();
        GetFollowingArtistsCommand.Cancel();
        CurrentUser?.Dispose();
        CurrentUser = null;

        foreach (var track in TopTracks)
        {
            track.Dispose();
        }
        TopTracks.Clear();        
        
        foreach (var artist in TopArtists)
        {
            artist.Dispose();
        }
        TopArtists.Clear();

        foreach (var followingArtist in FollowingArtists)
        {
            followingArtist.Dispose();
        }
        FollowingArtists.Clear();
    }
}