using System.Globalization;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SpotifyApp.Api.Client.DI;
using SpotifyApp.Shared.DI;
using SpotifyApp.Storage;
using MainView = SpotifyApp.Shared.Views.MainView;
using MainWindow = SpotifyApp.Shared.Views.MainWindow;

namespace SpotifyApp.Shared;

public sealed class App : Application
{
    private IServiceProvider? _serviceProvider;
    private readonly IServiceCollection _serviceCollection;

    /// <summary>
    /// Default ctor
    /// </summary>
    public App()
    {
        _serviceCollection = new ServiceCollection();
    }

    /// <summary>
    /// ctor with additional dependencies
    /// </summary>
    public App(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        RegisterDi();
        CreateDatabase();
        SetupLocalization();

        switch (ApplicationLifetime)
        {
            case IClassicDesktopStyleApplicationLifetime desktop:
                desktop.MainWindow = new MainWindow();
                break;
            case ISingleViewApplicationLifetime singleViewPlatform:
                singleViewPlatform.MainView = new MainView();
                break;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void RegisterDi()
    {
        _serviceCollection.AddAppDi();
        _serviceProvider = _serviceCollection.BuildServiceProvider();
        
        Ioc.Default.ConfigureServices(_serviceProvider);
    }

    private static void SetupLocalization()
    {
        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
    }
    
    private void CreateDatabase()
    {
        if (_serviceProvider?.GetRequiredService<IApplicationContext>() is DbContext context)
        {
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            context.Database.CloseConnection();
        }
    }
}