using System.Collections.ObjectModel;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.ViewModels.Base;
using SpotifyApp.Shared.ViewModels.Items;
using Type = SpotifyApp.Api.Client.OpenApiClient.Type;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class LibraryViewModel : ObservableRecipient
{
    private readonly ISpotifyClient _spotifyClient;
    private readonly IMapper _mapper;
    
    [ObservableProperty] 
    private ObservableCollection<ISpotifyItemViewModel> _items = new();

    public LibraryViewModel()
    {
        //Designer constructor
    }
    
    public LibraryViewModel(ISpotifyClient spotifyClient,
        IMapper mapper)
    {
        //Designer constructor
        _spotifyClient = spotifyClient;
        _mapper = mapper;
        IsActive = true;
    }

    protected override void OnActivated()
    {
        base.OnActivated();

        GetLibraryCommand.ExecuteAsync(null);
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetLibraryAsync(CancellationToken token)
    {
        await GetPlaylists(token);
        await GetAlbums(token);
        await GetArtists(token);
    }

    private async Task GetPlaylists(CancellationToken token)
    {
        var currentItemsCount = Items.Count;
        var playlists = await _spotifyClient.GetAListOfCurrentUsersPlaylistsAsync(null, currentItemsCount, token);

        foreach (var playlist in playlists.Items)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }
            
            var playlistVm = Ioc.Default.GetRequiredService<PlaylistViewModel>();
            Items.Add(playlistVm);
            var playlistModel = _mapper.Map<PlaylistModel>(playlist);
            ++currentItemsCount;
            playlistVm.Item = playlistModel;
        }
    }
    
    private async Task GetAlbums(CancellationToken token)
    {
        var currentItemsCount = Items.Count;
        var albums = await _spotifyClient.GetUsersSavedAlbumsAsync(null, currentItemsCount, null, token);

        foreach (var album in albums.Items)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }
            
            var albumVm = Ioc.Default.GetRequiredService<AlbumViewModel>();
            Items.Add(albumVm);
            var albumModel = _mapper.Map<AlbumModel>(album);
            ++currentItemsCount;
            albumVm.Item = albumModel;
        }
    }
    
    private async Task GetArtists(CancellationToken token)
    {
        var currentItemsCount = Items.Count;
        var artists = await _spotifyClient.GetUsersTopArtistsAndTracksAsync(Type.Artists, null, currentItemsCount, null, token);

        foreach (var artist in artists.Items)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }
            
            var artistVm = Ioc.Default.GetRequiredService<ArtistViewModel>();
            Items.Add(artistVm);
            var artistModel = _mapper.Map<ArtistModel>(artist);
            ++currentItemsCount;
            artistVm.Item = artistModel;
        }
    }
    
    protected override void OnDeactivated()
    {
        GetLibraryCommand.Cancel();
        foreach (var playlist in Items)
        {
            playlist.Dispose();
        }
        Items.Clear();
        base.OnDeactivated();
    }
}