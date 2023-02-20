using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SpotifyApp.Api.Client.Auth;
using SpotifyApp.Api.Client.DI;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Shared.AutoMapper;
using SpotifyApp.Shared.AutoMapper.Resolvers;
using SpotifyApp.Shared.Configurations;
using SpotifyApp.Shared.Services;
using SpotifyApp.Shared.ViewModels;
using SpotifyApp.Shared.ViewModels.Items;
using SpotifyApp.Storage.DI;

namespace SpotifyApp.Shared.DI;

/// <summary>
/// Extensions with service collections
/// </summary>
public static class AppServiceCollectionExtensions
{
    public static IServiceCollection AddAppDi(this IServiceCollection services)
    {
        return services.AddLogging(builder => builder.AddSerilog())
            .AddMemoryCache()
            .AddDatabase()
            .AddAutomapper()
            .AddApiClients()
            
            // Models shared across component - register as singleton
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<CurrentUserViewModel>()
            .AddSingleton<PlayerViewModel>()
            
            // Page models
            .AddTransient<ProfileViewModel>()
            .AddTransient<LikedSongsViewModel>()
            .AddTransient<ArtistScreenViewModel>()
            
            // Item models
            .AddTransient<TrackViewModel>()
            .AddTransient<UserViewModel>()
            .AddTransient<ArtistViewModel>()
            .AddTransient<PlaylistViewModel>()
            .AddTransient<AlbumWithTracksViewModel>()
            
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ITokenService, TokenService>()
            .AddSingleton<IOidcConfiguration, OidcConfiguration>()
            .AddSingleton<IImageCache, ImageCache>()
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