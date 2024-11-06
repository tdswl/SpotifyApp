using Microsoft.Extensions.DependencyInjection;
using SpotifyApp.Api.Client.DI;
using SpotifyApp.Api.Client.OpenApiClient;
using SpotifyApp.Repositories.DI;
using SpotifyApp.Services.Contracts;

namespace SpotifyApp.Services.DI;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddApiClients()

            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ITokenService, TokenService>()
            .AddSingleton<IImageCache, ImageCache>();

        return services;
    }
}