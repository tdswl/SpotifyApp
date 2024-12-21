using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Services.Contracts;
using SpotifyApp.Shared.Enums;

namespace SpotifyApp.Shared.ViewModels.Base;

public sealed partial class ImageViewModel : ViewModelWithInitialization, IDisposable
{
    private readonly IImageCache _imageCache;
    private readonly ICollection<ImageObject>? _spotifyImages;
    private readonly ImageSize _imageSize;
    
    [ObservableProperty] 
    private Bitmap? _image;

    public ImageViewModel(ICollection<ImageObject>? spotifyImages,
        ImageSize imageSize)
    {
        _imageCache = Ioc.Default.GetRequiredService<IImageCache>();
        _spotifyImages = spotifyImages;
        _imageSize = imageSize;
    }

    protected override async Task InitializeInternal(CancellationToken cancellationToken = default)
    {
        if (_spotifyImages is { Count: > 0 })
        {
            var imageWebUrl = GetImage(_spotifyImages, _imageSize).Url;
            var cachedFile = await _imageCache.GetCachedImagePath(imageWebUrl, cancellationToken);
            // Cleanup old image if it exists
            Dispose();
            // Set new one
            using (var memoryStream = new MemoryStream(cachedFile))
            {
                Image = new Bitmap(memoryStream);
            }
        }
    }
    
    private static ImageObject GetImage(ICollection<ImageObject> images, ImageSize imageSize)
    {
        var orderedImages = images.OrderBy(x => x.Width).ToList();
        return imageSize switch
        {
            ImageSize.Small => orderedImages.First(),
            ImageSize.Large => orderedImages.Last(),
            _ => orderedImages[images.Count / 2]
        };
    }

    public void Dispose()
    {
        Image?.Dispose(); 
    }
}