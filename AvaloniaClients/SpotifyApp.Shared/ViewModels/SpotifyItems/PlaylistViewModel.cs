using CommunityToolkit.Mvvm.ComponentModel;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.ViewModels.SpotifyItems;

public sealed partial class PlaylistViewModel : ObservableObject
{
    [ObservableProperty] 
    private string? _name;
    
    [ObservableProperty] 
    private string? _author;
    
    [ObservableProperty] 
    private ImageViewModel _image;

    public PlaylistViewModel()
    {
        // Designer constructor
    }
    
    public PlaylistViewModel(SimplifiedPlaylistObject playlist)
    {
        Name = playlist.Name;
        Author = playlist.Owner.Display_name;
        Image = new ImageViewModel(playlist.Images);
    }
}