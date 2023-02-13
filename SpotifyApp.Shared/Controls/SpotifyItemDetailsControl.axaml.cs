using Avalonia;
using Avalonia.Controls;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.Controls;

public class SpotifyItemDetailsControl : ContentControl
{
    /// <summary>
    /// Defines the <see cref="Header"/> property.
    /// </summary>
    public static readonly StyledProperty<ISpotifyItemViewModel?> HeaderProperty =
        AvaloniaProperty.Register<SpotifyItemDetailsControl, ISpotifyItemViewModel?>(nameof(Header));
    
    /// <summary>
    /// Gets or sets the content to display.
    /// </summary>
    public ISpotifyItemViewModel? Header
    {
        get { return GetValue(HeaderProperty); }
        set { SetValue(HeaderProperty, value); }
    }
}