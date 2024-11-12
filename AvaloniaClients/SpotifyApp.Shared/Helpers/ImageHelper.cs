using Avalonia.Media.Imaging;

namespace SpotifyApp.Shared.Helpers;

internal sealed class ImageHelper
{
    public static Bitmap LoadFromFile(string filePath)
    {
        return new Bitmap(filePath);
    }
}