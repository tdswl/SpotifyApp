using CommunityToolkit.Mvvm.ComponentModel;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.ViewModels.SpotifyItems;

public sealed partial class TrackViewModel : SpotifyItemBaseViewModel
{
    [ObservableProperty] 
    private string _name;
    
    [ObservableProperty] 
    private string _author;
    
    [ObservableProperty] 
    private string _album;

    public TrackViewModel()
    {
        // Designer constructor
    }
    
    public TrackViewModel(TrackObject track)
    {
        Id = track.Id;
        Name = track.Name;
        Author = string.Join(", ", track.Artists.Select(x => x.Name));
        Album = track.Album.Name;
        Image = new ImageViewModel(track.Album.Images, ImageSize.Small);
        Type = SpotifyItemType.Track;
    }
    
    public TrackViewModel(SimplifiedTrackObject track)
    {
        Id = track.Id;
        Name = track.Name;
        Author = string.Join(", ", track.Artists.Select(x => x.Name));
        Type = SpotifyItemType.Track;
    }
}