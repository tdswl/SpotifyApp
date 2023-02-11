using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SpotifyApp.Storage.OptionsFactories;

namespace SpotifyApp.Storage.DI;

public static class StorageServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.TryAdd(ServiceDescriptor.Singleton<IContextOptionsFactory, SqliteContextOptionsFactory>());
        services.TryAdd(ServiceDescriptor.Scoped<IApplicationContext, ApplicationContext>());

        return services;
    }
}