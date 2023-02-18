using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SpotifyApp.Api.Client.DI;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Api.Contracts.Auth;
using SpotifyApp.Shared.AutoMapper;
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
            .AddAutoMapper(AutoMapperConfig)
            .AddApiClients()
            .AddTransient<MainWindowViewModel>()
            .AddTransient<ProfileViewModel>()
            .AddTransient<LikedSongsViewModel>()
            .AddTransient<ArtistScreenViewModel>()
            
            .AddTransient<TrackViewModel>()
            .AddTransient<UserViewModel>()
            .AddTransient<ArtistViewModel>()
            .AddTransient<PlaylistViewModel>()
            .AddTransient<AlbumWithTracksViewModel>()

            .AddSingleton<IOidcConfiguration, OidcConfiguration>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ITokenService, TokenService>()
            .AddSingleton<IImageCache, ImageCache>()
            .AddSingleton<INavigationService, NavigationService>();
    }

    private static void AutoMapperConfig(IMapperConfigurationExpression cfg)
    {
        cfg.AddProfile<ApplicationMapProfile>();
    }
}