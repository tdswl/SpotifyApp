using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SpotifyApp.Repositories.Contracts;

namespace SpotifyApp.Repositories.DI;

public static class RepositoriesServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.TryAdd(ServiceDescriptor.Scoped<Contracts.IOAuthTokenWriteRepository, OAuthTokenWriteRepository>());
        services.TryAdd(ServiceDescriptor.Scoped<Contracts.IOAuthTokenReadRepository, OAuthTokenReadRepository>());

        return services;
    }
}