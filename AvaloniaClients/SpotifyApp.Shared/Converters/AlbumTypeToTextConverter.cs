using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Properties;

namespace SpotifyApp.Shared.Converters;

public sealed class AlbumTypeToTextConverter : IValueConverter
{
    public static readonly AlbumTypeToTextConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture )
    {
        if (value is AlbumType sourceAlbumType && 
            targetType.IsAssignableTo(typeof(string)))
        {
            return sourceAlbumType switch
            {
                AlbumType.Single => Resources.AlbumTypeSingle.ToUpperInvariant(),
                AlbumType.Compilation => Resources.AlbumTypeCompilation.ToUpperInvariant(),
                AlbumType.Album => Resources.AlbumTypeAlbum.ToUpperInvariant(),
                _ => throw new ArgumentOutOfRangeException(nameof(sourceAlbumType),
                    sourceAlbumType,
                    $"Unknown value in {nameof(AlbumTypeToTextConverter)}"),
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