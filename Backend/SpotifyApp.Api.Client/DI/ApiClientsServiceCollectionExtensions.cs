using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SpotifyApp.Api.Client.Auth;
using SpotifyApp.Api.Client.OpenApiClient;

namespace SpotifyApp.Api.Client.DI;

/// <summary>
/// Extensions with service collections
/// </summary>
public static class ApiClientsServiceCollectionExtensions
{
    /// <summary>
    /// Add spotify api clients to service collection
    /// </summary>
    public static IServiceCollection AddApiClients(this IServiceCollection services)
    {
        services.TryAdd(ServiceDescriptor.Singleton<IAuthClient, AuthClient>());
        
        services.AddHttpClient();
        services.TryAdd(ServiceDescriptor.Singleton<ISpotifyClient, SpotifyClient>());
        
        return services;
    }
}