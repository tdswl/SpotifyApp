using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Services.Contracts;

namespace SpotifyApp.Shared.ViewModels.Base;

public sealed partial class ImageViewModel : ObservableObject, IDisposable
{
    private readonly IImageCache _imageCache;
    private readonly ICollection<ImageObject>? _spotifyImages;

    [ObservableProperty] 
    private Bitmap? _image;

    public ImageViewModel(ICollection<ImageObject>? spotifyImages)
    {
        _imageCache = Ioc.Default.GetRequiredService<IImageCache>();
        _spotifyImages = spotifyImages;
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task LoadImageAsync(CancellationToken token)
    {
        if (_spotifyImages is { Count: > 0 })
        {
            var imageWebUrl = GetImage(_spotifyImages)?.Url;
            var cachedUrl = await _imageCache.GetCachedImagePath(imageWebUrl, token);
            // Cleanup old image if it exists
            CleanupCommand.Execute(null);
            // Set new one
            Image = new Bitmap(cachedUrl);
        }
    }
    
    [RelayCommand]
    private void Cleanup()
    {
        Dispose();
    }
    
    private static ImageObject? GetImage(ICollection<ImageObject> images)
    {
        return images.Count switch
        {
            0 => null,
            1 => images.First(),
            _ => images.OrderBy(a => a.Width).ToList()[images.Count / 2]
        };
    }

    public void Dispose()
    {
        Image?.Dispose(); 
    }
}