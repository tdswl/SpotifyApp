using Avalonia;
using Avalonia.Controls;
using Avalonia.Xaml.Interactivity;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.Behaviors;

public sealed class InitializationBehavior : Behavior<Visual>
{
    protected override void OnAttached()
    {
        base.OnAttached();

        if (Design.IsDesignMode)
        {
            return;
        }

        if (AssociatedObject is not null)
        {
            AssociatedObject.AttachedToVisualTree += AssociatedObjectOnAttachedToVisualTree;
            AssociatedObject.DetachedFromVisualTree += AssociatedObjectOnDetachedFromVisualTree;
        }
        
        if (AssociatedObject?.DataContext is ViewModelWithInitialization initialization)
        {
            initialization.Initialize();
        }
    }

    protected override void OnDetaching()
    {
        if (AssociatedObject is not null)
        {
            AssociatedObject.AttachedToVisualTree -= AssociatedObjectOnAttachedToVisualTree;
            AssociatedObject.DetachedFromVisualTree -= AssociatedObjectOnDetachedFromVisualTree;
        }

        if (AssociatedObject?.DataContext is IDisposable disposable)
        {
            disposable.Dispose();
        }
        
        base.OnDetaching();
    }
    
    
    private void AssociatedObjectOnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        if (AssociatedObject?.DataContext is ViewModelWithInitialization initialization)
        {
            initialization.Initialize();
        }
    }

    private void AssociatedObjectOnDetachedFromVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        if (AssociatedObject?.DataContext is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}