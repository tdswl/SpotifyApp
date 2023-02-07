using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SpotifyApp.Api.Client.Users;

namespace SpotifyApp.Api.Client.DI;

public static class ApiClientsServiceCollectionExtensions
{
    public static IServiceCollection AddApiClients(this IServiceCollection services)
    {
        services.TryAdd(ServiceDescriptor.Scoped<IUsersClient, UsersClient>());

        return services;
    }
}