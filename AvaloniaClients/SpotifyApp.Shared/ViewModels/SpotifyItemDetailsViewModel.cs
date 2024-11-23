using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.ViewModels.Base;
using SpotifyApp.Shared.ViewModels.SpotifyItems;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class SpotifyItemDetailsViewModel : ViewModelWithInitialization
{
    private readonly ISpotifyClient _spotifyClient;
    
    [ObservableProperty] 
    private string _id;
    
    [ObservableProperty] 
    private string _name;
    
    [ObservableProperty] 
    private string _author;
    
    [ObservableProperty] 
    private ImageViewModel _image;
    
    [ObservableProperty] 
    private SpotifyItemType _type;
    
    [ObservableProperty] 
    private ObservableCollection<SpotifyItemBaseViewModel> _items = [];

    public SpotifyItemDetailsViewModel()
    {
        // Designer constructor
    }
    
    public SpotifyItemDetailsViewModel(ISpotifyClient spotifyClient)
    {
        _spotifyClient = spotifyClient;
    }

    protected override async Task Initialize(CancellationToken cancellationToken = default)
    {
        switch (Type)
        {
            case SpotifyItemType.Playlist:
                var playlist = await _spotifyClient.GetPlaylistAsync(Id, null, null, null, cancellationToken);
                Name = playlist.Name;
                Author = playlist.Owner.Display_name;
                Image = new ImageViewModel(playlist.Images, ImageSize.Large);
                foreach (var track in playlist.Tracks.Items)
                {
                    Items.Add(new TrackViewModel(track.Track));
                }
                break;
            case SpotifyItemType.Album:
                var album = await _spotifyClient.GetAnAlbumAsync(Id, null, cancellationToken);
                Name = album.Name;
                Author = string.Join(", ", album.Artists.Select(x => x.Name));
                Image = new ImageViewModel(album.Images, ImageSize.Large);
                foreach (var track in album.Tracks.Items)
                {
                    Items.Add(new TrackViewModel(track));
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected override Task Deactivate(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}