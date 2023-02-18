using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Properties;

namespace SpotifyApp.Shared.Converters;

public class AlbumGroupToTextConverter : IValueConverter
{
    public static readonly AlbumGroupToTextConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture )
    {
        if (value is AlbumGroup sourceAlbumGroup && 
            targetType.IsAssignableTo(typeof(string)))
        {
            return sourceAlbumGroup switch
            {
                AlbumGroup.AppearsOn => Resources.AlbumGroupAppearsOn.ToUpperInvariant(),
                AlbumGroup.Single => Resources.AlbumGroupSingle.ToUpperInvariant(),
                AlbumGroup.Compilation => Resources.AlbumGroupCompilation.ToUpperInvariant(),
                AlbumGroup.Album => Resources.AlbumGroupAlbum.ToUpperInvariant(),
                _ => throw new ArgumentOutOfRangeException(nameof(sourceAlbumGroup),
                    sourceAlbumGroup,
                    $"Unknown value in {nameof(AlbumGroupToTextConverter)}"),
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