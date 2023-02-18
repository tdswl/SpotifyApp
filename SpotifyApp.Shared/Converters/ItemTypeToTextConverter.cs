using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using SpotifyApp.Shared.Enums;
using SpotifyApp.Shared.Properties;

namespace SpotifyApp.Shared.Converters;

public class ItemTypeToTextConverter : IValueConverter
{
    public static readonly ItemTypeToTextConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture )
    {
        if (value is ItemType sourceItemType && 
            targetType.IsAssignableTo(typeof(string)))
        {
            return sourceItemType switch
            {
                ItemType.Artist => Resources.ItemTypeArtist.ToUpperInvariant(),
                ItemType.Track => Resources.ItemTypeTrack.ToUpperInvariant(),
                ItemType.User => Resources.ItemTypeUser.ToUpperInvariant(),
                ItemType.Album => Resources.ItemTypeAlbum.ToUpperInvariant(),
                ItemType.AudioFeatures => Resources.ItemTypeAudioFeatures.ToUpperInvariant(),
                ItemType.Genre => Resources.ItemTypeGenre.ToUpperInvariant(),
                ItemType.Playlist => Resources.ItemTypePlaylist.ToUpperInvariant(),
                _ => throw new ArgumentOutOfRangeException(nameof(sourceItemType),
                    sourceItemType,
                    "Unknown value in ItemTypeToTextConverter"),
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