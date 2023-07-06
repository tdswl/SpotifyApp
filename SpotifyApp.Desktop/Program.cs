using Avalonia;
using System;
using Microsoft.Extensions.DependencyInjection;
using SpotifyApp.Desktop.DI;
using SpotifyApp.Shared;

namespace SpotifyApp.Desktop;

internal class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure(AppFactory)
            .WithInterFont()
            .UsePlatformDetect()
            .LogToTrace();

    private static App AppFactory()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDesktop();
        return new App(serviceCollection);
    }
}