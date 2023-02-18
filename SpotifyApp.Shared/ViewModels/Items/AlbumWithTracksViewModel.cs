using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SpotifyApp.Shared.Services;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.ViewModels.Items;

public sealed partial class AlbumWithTracksViewModel : ImagePreviewViewModel
{
    [ObservableProperty] 
    private ObservableCollection<TrackViewModel> _tracks = new();
    
    public AlbumWithTracksViewModel(IImageCache imageCache) : base(imageCache)
    {
    }
    
}