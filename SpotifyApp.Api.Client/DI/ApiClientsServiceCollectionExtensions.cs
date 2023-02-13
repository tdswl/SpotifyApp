using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SpotifyApp.Api.Client.Artists;
using SpotifyApp.Api.Client.Tracks;
using SpotifyApp.Api.Client.Users;

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
        services.TryAdd(ServiceDescriptor.Scoped<IUsersClient, UsersClient>());
        services.TryAdd(ServiceDescriptor.Scoped<ITracksClient, TracksClient>());
        services.TryAdd(ServiceDescriptor.Scoped<IArtistsClient, ArtistsClient>());
        
        return services;
    }
}