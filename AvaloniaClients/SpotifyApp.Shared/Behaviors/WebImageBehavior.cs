using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.DependencyInjection;
using SpotifyApp.Services.Contracts;
using SpotifyApp.Shared.Helpers;

namespace SpotifyApp.Shared.Behaviors;

public sealed class WebImageBehavior : AvaloniaObject
{
    public static readonly AttachedProperty<string> WebUrlProperty = 
        AvaloniaProperty.RegisterAttached<WebImageBehavior, Image, string>("WebUrl");

    public static void SetWebUrl(AvaloniaObject element, string value)
    {
        element.SetValue(WebUrlProperty, value);
    }

    public static string GetWebUrl(AvaloniaObject element)
    {
        return element.GetValue(WebUrlProperty);
    }
    
    static WebImageBehavior()
    {
        if (Design.IsDesignMode)
        {
            return;
        }
        
        WebUrlProperty.Changed.AddClassHandler<Image>(WebUrlPropertyChanged);
    }

    private static async void WebUrlPropertyChanged(Image control, AvaloniaPropertyChangedEventArgs args)
    {
        try
        {
            if (args.NewValue is string webUrl && !string.IsNullOrWhiteSpace(webUrl))
            {
                control.AddHandler(Control.UnloadedEvent, UnloadedHandler);
            
                var imageCache = Ioc.Default.GetRequiredService<IImageCache>();
                var cachedUrl = await imageCache.GetCachedImagePath(webUrl, default);
                // Cleanup old image if it exists
                Cleanup(control);
                // Set new one
                control.Source = ImageHelper.LoadFromFile(cachedUrl);
            }
        }
        catch (Exception)
        {
            // Do nothing
        }
    }
    
    private static void UnloadedHandler(object? sender, RoutedEventArgs e)
    {
        if (sender is Image control)
        {
            control.RemoveHandler(Control.UnloadedEvent, UnloadedHandler);
            Cleanup(control);
        }
    }

    private static void Cleanup(Image control)
    {
        if (control.Source is Bitmap bitmap)
        {
            bitmap.Dispose();
        }
    }
}