using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SpotifyApp.Storage.DI;

public static class StorageServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.TryAdd(ServiceDescriptor.Scoped<IApplicationContext, ApplicationContext>());

        return services;
    }
}