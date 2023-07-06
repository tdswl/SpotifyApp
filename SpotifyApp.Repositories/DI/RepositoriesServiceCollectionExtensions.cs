using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SpotifyApp.Repositories.Contracts;

namespace SpotifyApp.Repositories.DI;

public static class RepositoriesServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.TryAdd(ServiceDescriptor.Scoped<IUserSettingsWriteRepository, UserSettingsWriteRepository>());
        services.TryAdd(ServiceDescriptor.Scoped<IUserSettingsReadRepository, UserSettingsReadRepository>());

        return services;
    }
}