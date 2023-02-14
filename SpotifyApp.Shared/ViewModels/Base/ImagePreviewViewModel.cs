using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Shared.Models;
using SpotifyApp.Shared.Services;
using Svg.Skia;

namespace SpotifyApp.Shared.ViewModels.Base;

public abstract partial class ImagePreviewViewModel : ObservableRecipient, ISpotifyItemViewModel
{
    private const int DefaultImageWidth = 400;
    private readonly IImageCache _imageCache;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoadAdditionalInfoCommand))]
    private ISpotifyItem? _item;

    [ObservableProperty]
    private Bitmap? _preview;

    protected ImagePreviewViewModel(IImageCache imageCache)
    {
        _imageCache = imageCache;
    }
    
    partial void OnItemChanged(ISpotifyItem? value)
    {
        if (value != null)
        {
            LoadAdditionalInfoCommand.ExecuteAsync(null);
        }
    }
    
    [RelayCommand(IncludeCancelCommand = true, CanExecute = nameof(LoadAdditionalInfoCanExecute))]
    private Task LoadAdditionalInfoAsync(CancellationToken token)
    {
        return LoadPreview(token);
    }

    private bool LoadAdditionalInfoCanExecute()
    {
        return Item != null;
    }

    private async Task LoadPreview(CancellationToken token)
    {
        var previewImage = (Item?.Images).MaxBy(a => a.Width);
        if (previewImage != null)
        {
            var imagePath = await _imageCache.GetImage(previewImage.Url, token);

            await using (var fileStream = File.OpenRead(imagePath))
            {
                var width = previewImage.Width ?? DefaultImageWidth;
                Preview = await Task.Run(() => Bitmap.DecodeToWidth(fileStream, width), token);
            }
        }
        else
        {
            var svg = new SKSvg();
            svg.Load("image.svg");

        }
    }
}