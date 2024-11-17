using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using SpotifyApp.Shared.ViewModels.Base;

namespace SpotifyApp.Shared.Behaviors;

public sealed class InitializeBehavior : AvaloniaObject
{
    public static readonly AttachedProperty<bool> IsEnabledProperty = 
        AvaloniaProperty.RegisterAttached<InitializeBehavior, Control, bool>("IsEnabled");

    public static void SetIsEnabled(AvaloniaObject element, bool value)
    {
        element.SetValue(IsEnabledProperty, value);
    }

    public static bool GetIsEnabled(AvaloniaObject element)
    {
        return element.GetValue(IsEnabledProperty);
    }
    
    static InitializeBehavior()
    {
        if (Design.IsDesignMode)
        {
            return;
        }
            
        IsEnabledProperty.Changed.AddClassHandler<Control>(Action);
    }

    private static void Action(Control control, AvaloniaPropertyChangedEventArgs args)
    {
        if (args.NewValue is true)
        {
            control.AddHandler(Control.LoadedEvent, LoadedHandler);
            control.AddHandler(Control.UnloadedEvent, UnloadedHandler);
        }
    }

    private static void LoadedHandler(object? sender, RoutedEventArgs e)
    {
        Initialize(sender);
    }
    
    private static void UnloadedHandler(object? sender, RoutedEventArgs e)
    {
        if (sender is Control control)
        {
            control.RemoveHandler(Control.LoadedEvent, LoadedHandler);
            control.RemoveHandler(Control.UnloadedEvent, UnloadedHandler);

            if (control.DataContext is ViewModelWithInitialization viewModel)
            {
                viewModel.DeactivateCommand.Execute(default);
            }
        }
    }
    
    private static void Initialize(object? sender)
    {
        if (sender is Control { DataContext: ViewModelWithInitialization viewModel })
        {
            viewModel.InitializeCommand.Execute(default);
        }
    }
}