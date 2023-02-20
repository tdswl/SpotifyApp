using System.Collections.ObjectModel;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.ViewModels.Items;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class ArtistScreenViewModel : ObservableRecipient
{
    private readonly ISpotifyClient _spotifyClient;
    private readonly IMapper _mapper;

    [ObservableProperty] 
    private ArtistViewModel? _artist;
    
    [ObservableProperty] 
    private ObservableCollection<AlbumWithTracksViewModel> _albums = new();
    
    public ArtistScreenViewModel()
    {
        //Designer constructor
    }
    
    public ArtistScreenViewModel(ISpotifyClient spotifyClient,
        IMapper mapper)
    {
        _spotifyClient = spotifyClient;
        _mapper = mapper;
        IsActive = true;
    }

    protected override void OnActivated()
    {
        base.OnActivated();
    }
    
    partial void OnArtistChanged(ArtistViewModel? value)
    {
        if (value?.Item != null)
        {
            GetAlbumsCommand.ExecuteAsync(value.Item.Id);
        }
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetArtistAsync(string id, CancellationToken token)
    {
        var artistResponse = await _spotifyClient.GetAnArtistAsync(id, token);
     
        if (token.IsCancellationRequested)
        {
            return;
        }

        var artistVm = Ioc.Default.GetRequiredService<ArtistViewModel>();
        var artist = _mapper.Map<ArtistModel>(artistResponse);
        artistVm.PreviewSize = PreviewSize.Large;
        artistVm.Item = artist;
        Artist = artistVm;
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetAlbumsAsync(string artistId, CancellationToken token)
    {
        var albumsResponse = await _spotifyClient.GetAnArtistsAlbumsAsync(artistId, null, null, null, null, token);
        
        if (token.IsCancellationRequested)
        {
            return;
        }
        
        foreach (var albumItem in albumsResponse.Items)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }
            
            var albumVm = Ioc.Default.GetRequiredService<AlbumWithTracksViewModel>();
            Albums.Add(albumVm);
            var album = _mapper.Map<AlbumModel>(albumItem);
            albumVm.Item = album;
            albumVm.GetAlbumTracksCommand.ExecuteAsync(albumItem.Id);
        }
    }
    

    protected override void OnDeactivated()
    {
        GetAlbumsCommand.Cancel();
        GetArtistCommand.Cancel();
        foreach (var album in Albums)
        {
            album.Dispose();
        }
        Albums.Clear();
        Artist?.Dispose();
        Artist = null;
    }
}