using Avalonia.Media.Imaging;
using SpotifyApp.Shared.Models;

namespace SpotifyApp.Shared.ViewModels.Base;

public interface ISpotifyItemViewModel
{
    ISpotifyItem? Item { get; set; }
    
    Bitmap? Preview  { get; set; }
}