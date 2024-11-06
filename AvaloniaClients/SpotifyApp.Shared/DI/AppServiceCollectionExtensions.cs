using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SpotifyApp.Api.Client.Auth;
using SpotifyApp.Api.Client.DI;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Repositories.DI;
using SpotifyApp.Services.DI;
using SpotifyApp.Shared.AutoMapper;
using SpotifyApp.Shared.AutoMapper.Resolvers;
using SpotifyApp.Shared.Configurations;
using SpotifyApp.Shared.Services;
using SpotifyApp.Shared.ViewModels;
using SpotifyApp.Shared.ViewModels.Items;

namespace SpotifyApp.Shared.DI;

/// <summary>
/// Extensions with service collections
/// </summary>
public static class AppServiceCollectionExtensions
{
    public static IServiceCollection AddAppDi(this IServiceCollection services)
    {
        return services
            .AddLogging(builder => builder.AddSerilog())
            .AddMemoryCache()
            .AddAutomapper()
            .AddServices()
            
            // Models shared across component - register as singleton
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<NavigateViewModel>()
            .AddSingleton<CurrentUserViewModel>()
            .AddSingleton<PlayerViewModel>()
            .AddSingleton<LibraryViewModel>()
            
            // Page models
            .AddTransient<ProfileViewModel>()
            .AddTransient<LikedSongsViewModel>()
            .AddTransient<ArtistScreenViewModel>()
            .AddTransient<SearchViewModel>()
            
            // Item models
            .AddTransient<TrackViewModel>()
            .AddTransient<UserViewModel>()
            .AddTransient<ArtistViewModel>()
            .AddTransient<PlaylistViewModel>()    
            .AddTransient<AlbumViewModel>()
            .AddTransient<AlbumWithTracksViewModel>()
            
            .AddSingleton<IOidcConfiguration, OidcConfiguration>()
            .AddSingleton<INavigationService, NavigationService>();
    }

    private static IServiceCollection AddAutomapper(this IServiceCollection services)
    {
        services = services.AddAutoMapper(AutoMapperConfig)
            .AddSingleton<ProductToEnumResolver>();

        return services;
    }

    private static void AutoMapperConfig(IMapperConfigurationExpression cfg)
    {
        cfg.AddProfile<ApplicationMapProfile>();
    }
}