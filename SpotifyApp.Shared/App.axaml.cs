using AutoMapper;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SpotifyApp.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SpotifyApp.Api.Client.DI;
using SpotifyApp.Shared.AutoMapper;
using SpotifyApp.Shared.ViewModels.Items;
using SpotifyApp.Storage;
using SpotifyApp.Storage.DI;
using MainView = SpotifyApp.Shared.Views.MainView;
using MainWindow = SpotifyApp.Shared.Views.MainWindow;
using MainWindowViewModel = SpotifyApp.Shared.ViewModels.MainWindowViewModel;
using ProfileViewModel = SpotifyApp.Shared.ViewModels.ProfileViewModel;

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
        _serviceCollection
            .AddLogging(builder => builder.AddSerilog())
            .AddMemoryCache()
            .AddDatabase()
            .AddAutoMapper(AutoMapperConfig)
            .AddApiClients()
            .AddTransient<MainWindowViewModel>()
            .AddTransient<ProfileViewModel>()
            .AddTransient<TrackViewModel>()
            .AddTransient<UserViewModel>()
            .AddTransient<ArtistViewModel>()

            .AddScoped<IAuthService, AuthService>()
            .AddSingleton<IImageCache, ImageCache>()
            .AddSingleton<INavigationService, NavigationService>();
        
        _serviceProvider = _serviceCollection.BuildServiceProvider();
        
        Ioc.Default.ConfigureServices(_serviceProvider);
    }
    
    private static void AutoMapperConfig(IMapperConfigurationExpression cfg)
    {
        cfg.AddProfile<ApplicationMapProfile>();
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