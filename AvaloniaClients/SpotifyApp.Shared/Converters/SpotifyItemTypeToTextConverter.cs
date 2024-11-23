using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Properties;

namespace SpotifyApp.Shared.Converters;

public sealed class SpotifyItemTypeToTextConverter : IValueConverter
{
    public static readonly SpotifyItemTypeToTextConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture )
    {
        if (value is SpotifyItemType sourceItemType && targetType.IsAssignableTo(typeof(string)))
        {
            return sourceItemType switch
            {
                SpotifyItemType.Album => Resources.ItemTypeAlbum,
                SpotifyItemType.Playlist => Resources.ItemTypePlaylist,
                _ => throw new ArgumentOutOfRangeException(nameof(sourceItemType),
                    sourceItemType,
                    $"Unknown value in {nameof(SpotifyItemTypeToTextConverter)}"),
            };
        }
        
        // converter used for the wrong type
        return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    }

    public object ConvertBack( object? value, Type targetType, object? parameter, CultureInfo culture )
    {
        throw new NotSupportedException();
    }
}