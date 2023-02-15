using Avalonia.Media.Imaging;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Models;

namespace SpotifyApp.Shared.ViewModels.Base;

public interface ISpotifyItemViewModel
{
    ISpotifyItem? Item { get; set; }
    
    PreviewSize PreviewSize { get; set; }
    
    IBitmap? Preview  { get; set; }
}