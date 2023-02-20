using System.Collections.ObjectModel;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Messages;
using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.Models.NavigateParams;
using SpotifyApp.Shared.ViewModels.Items;
using Type = SpotifyApp.Api.Client.OpenApiClient.Type;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class ProfileViewModel : ObservableRecipient
{
    private readonly ISpotifyClient _spotifyClient;
    private readonly IMapper _mapper;
    
    [ObservableProperty]
    private CurrentUserViewModel? _currentUser;

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
    
    public ProfileViewModel(ISpotifyClient spotifyClient,
        IMapper mapper)
    {
        _spotifyClient = spotifyClient;
        _mapper = mapper;
        IsActive = true;
    }

    protected override void OnActivated()
    {
        base.OnActivated();

        GetUserInfoCommand.Execute(null);
        GetArtistsCommand.ExecuteAsync(null);
        GetTracksCommand.ExecuteAsync(null);
        GetFollowingArtistsCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    private void GetUserInfo()
    {
        CurrentUser = Ioc.Default.GetRequiredService<CurrentUserViewModel>();
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetArtistsAsync(CancellationToken token)
    {
        var artistsResponse = await _spotifyClient
            .GetUsersTopArtistsAndTracksAsync(Type.Artists, null, null, null, token);

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
        var topTracksResponse = await _spotifyClient
            .GetUsersTopArtistsAndTracksAsync(Type.Tracks, null, 4, null, token);

        // 50 - max by api
        var trackIds = string.Join(",", topTracksResponse.Items.Take(50).Select(a => a.Id));
        var tracksInfoResponse = await _spotifyClient.GetSeveralTracksAsync(null, trackIds, token);

        var currentItemsCount = 0; 
        foreach (var trackObject in tracksInfoResponse.Tracks)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }
            
            var trackVm = Ioc.Default.GetRequiredService<TrackViewModel>();
            TopTracks.Add(trackVm);
            var track = _mapper.Map<TrackModel>(trackObject);
            track.Index = ++currentItemsCount;
            trackVm.Item = track;
        }
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetFollowingArtistsAsync(CancellationToken token)
    {
        var artistsResponse = await _spotifyClient.GetFollowedAsync(Type2.Artist, null, null, token);

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
            OpenArtist(value.Item.Id);
        }
    }
    
    partial void OnSelectedFollowingArtistChanged(ArtistViewModel? value)
    {
        if (value != null)
        {
            OpenArtist(value.Item.Id);
        }
    }

    protected override void OnDeactivated()
    {
        GetArtistsCommand.Cancel();
        GetTracksCommand.Cancel();
        GetFollowingArtistsCommand.Cancel();
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