using System.Collections.ObjectModel;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Api.Client.Albums;
using SpotifyApp.Api.Contracts.Albums.Requests;
using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.Services;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.ViewModels.Items;

public sealed partial class AlbumWithTracksViewModel : ImagePreviewViewModel
{
    private readonly IAuthService _authService;
    private readonly IAlbumsClient _albumsClient;
    private readonly IMapper _mapper;
    
    [ObservableProperty] 
    private ObservableCollection<TrackViewModel> _tracks = new();
    
    public AlbumWithTracksViewModel(IImageCache imageCache,
        IAuthService authService,
        IAlbumsClient albumsClient,
        IMapper mapper) : base(imageCache)
    {
        _authService = authService;
        _albumsClient = albumsClient;
        _mapper = mapper;
    }
    
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetAlbumTracksAsync(AlbumWithTracksViewModel album, CancellationToken token)
    {
        var authInfo = await _authService.Login(token);
        var albumTracksResponse = await _albumsClient.GetAlbumTracks(
            new GetAlbumTracksRequest { Id = album.Item.Id, },
            authInfo.AccessToken,
            token);
        
        if (token.IsCancellationRequested)
        {
            return;
        }
        
        for (var i = 0; i < albumTracksResponse.Items.Count; i++)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }
            
            var trackVm = Ioc.Default.GetRequiredService<TrackViewModel>();
            album.Tracks.Add(trackVm);
            var track = _mapper.Map<TrackModel>(albumTracksResponse.Items[i]);
            track.Index = i + 1;
            trackVm.Item = track;
        }
    }

    protected override void Dispose(bool disposing)
    {
        GetAlbumTracksCommand.Cancel();
        foreach (var track in Tracks)
        {
            track.Dispose();
        }
        Tracks.Clear();
        
        base.Dispose(disposing);
    }
}