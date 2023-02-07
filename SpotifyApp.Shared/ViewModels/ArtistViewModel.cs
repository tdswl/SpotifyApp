using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpotifyApp.Api.Contracts.Users.Models;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Services;

namespace SpotifyApp.Shared.ViewModels;

public sealed partial class ArtistViewModel : ObservableRecipient
{
    private readonly IImageCache _imageCache;
    
    [ObservableProperty]
    private TopItemModel? _artist;

    [ObservableProperty]
    private Bitmap? _cover;

    public ArtistViewModel(IImageCache imageCache)
    {
        _imageCache = imageCache;
    }
    
    partial void OnArtistChanged(TopItemModel? value)
    {
        if (value != null)
        {
            LoadCoverCommand.ExecuteAsync(null);
        }
    }
    
    [RelayCommand(IncludeCancelCommand = true)]
    private async Task LoadCoverAsync(CancellationToken token)
    {
        var cover = _artist.Images.FirstOrDefault();
        if (cover != null)
        {
            var imagePath = await _imageCache.GetImage(_artist.Id,
                ImageType.Artist,
                cover.Url,
                token);

            await using (var fileStream = File.OpenRead(imagePath))
            {
                Cover = await Task.Run(() => Bitmap.DecodeToWidth(fileStream, 400), token);
            }
        }
    }
}