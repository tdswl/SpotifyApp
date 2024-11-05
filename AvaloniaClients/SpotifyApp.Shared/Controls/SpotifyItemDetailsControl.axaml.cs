using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
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
    /// Gets or sets the content of header to display.
    /// </summary>
    [DependsOn(nameof(HeaderTemplate))]
    public ISpotifyItemViewModel? Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }
    
    /// <summary>
    /// Defines the <see cref="HeaderTemplate"/> property.
    /// </summary>
    public static readonly StyledProperty<IDataTemplate?> HeaderTemplateProperty =
        AvaloniaProperty.Register<SpotifyItemDetailsControl, IDataTemplate?>(nameof(HeaderTemplate));
    
    /// <summary>
    /// Gets or sets the data template used to display the content of the control.
    /// </summary>
    public IDataTemplate? HeaderTemplate
    {
        get => GetValue(HeaderTemplateProperty);
        set => SetValue(HeaderTemplateProperty, value);
    }
}