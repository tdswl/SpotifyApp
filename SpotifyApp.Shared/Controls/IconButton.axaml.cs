using Avalonia;
using Avalonia.Controls;

namespace SpotifyApp.Shared.Controls;

public class IconButton : Button
{
    /// <summary>
    /// Defines the <see cref="Icon"/> property.
    /// </summary>
    public static readonly StyledProperty<string?> IconProperty =
        AvaloniaProperty.Register<IconButton, string?>(nameof(Icon));
    
    /// <summary>
    /// Gets or sets the content of header to display.
    /// </summary>
    public string? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
    
    /// <summary>
    /// Defines the <see cref="Text"/> property.
    /// </summary>
    public static readonly StyledProperty<string?> TextProperty =
        AvaloniaProperty.Register<IconButton, string?>(nameof(Text));
    
    /// <summary>
    /// Gets or sets the content of header to display.
    /// </summary>
    public string? Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}