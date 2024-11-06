using System.Collections.ObjectModel;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Services.Contracts;
using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.ViewModels.Items;

public sealed partial class AlbumWithTracksViewModel : ImagePreviewViewModel
{
    private readonly ISpotifyClient _spotifyClient;
    private readonly IMapper _mapper;
    
    [ObservableProperty] 
    private ObservableCollection<TrackViewModel> _tracks = new();
    
    public AlbumWithTracksViewModel(IImageCache imageCache,
        ISpotifyClient spotifyClient,
        IMapper mapper) : base(imageCache)
    {
        _spotifyClient = spotifyClient;
        _mapper = mapper;
        IsActive = true;
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task GetAlbumTracksAsync(string albumId, CancellationToken token)
    {
        var albumTracksResponse = await _spotifyClient.GetAnAlbumsTracksAsync(albumId, null, null, null, token);
      
        if (token.IsCancellationRequested)
        {
            return;
        }
        
        var currentItemsCount = 0; 
        foreach (var trackObject in albumTracksResponse.Items)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }
            
            var trackVm = Ioc.Default.GetRequiredService<TrackViewModel>();
            Tracks.Add(trackVm);
            var track = _mapper.Map<TrackModel>(trackObject);
            track.Index = ++currentItemsCount;
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