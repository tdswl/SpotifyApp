using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using SpotifyApp.Shared.Enums;

namespace SpotifyApp.Shared.Converters;

public class ItemTypeToTextConverter : IValueConverter
{
    public static readonly ItemTypeToTextConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture )
    {
        if (value is ItemType sourceItemType && 
            targetType.IsAssignableTo(typeof(string)))
        {
            return sourceItemType.ToString().ToUpperInvariant();
        }
        
        // converter used for the wrong type
        return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    }

    public object ConvertBack( object? value, Type targetType, object? parameter, CultureInfo culture )
    {
        throw new NotSupportedException();
    }
}