using CommunityToolkit.Mvvm.ComponentModel;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.ViewModels.SpotifyItems;

public sealed partial class AlbumViewModel : SpotifyItemBaseViewModel
{
    [ObservableProperty] 
    private string _name;
    
    [ObservableProperty] 
    private string _author;

    public AlbumViewModel()
    {
        // Designer constructor
    }
    
    public AlbumViewModel(SimplifiedPlaylistObject playlist)
    {
        Id = playlist.Id;
        Name = playlist.Name;
        Author = playlist.Owner.Display_name;
        Image = new ImageViewModel(playlist.Images, ImageSize.Small);
        Type = SpotifyItemType.Album;
    }
}