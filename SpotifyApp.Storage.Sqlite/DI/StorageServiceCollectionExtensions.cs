using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SpotifyApp.Storage.Contracts.Interfaces;
using SpotifyApp.Storage.Sqlite.OptionsFactories;

namespace SpotifyApp.Storage.Sqlite.DI;

public static class StorageServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.TryAdd(ServiceDescriptor.Singleton<IContextOptionsFactory, SqliteContextOptionsFactory>());
        services.TryAdd(ServiceDescriptor.Scoped<IStorageReader, ApplicationContext>());
        services.TryAdd(ServiceDescriptor.Scoped<IStorageWriter, ApplicationContext>());
        services.TryAdd(ServiceDescriptor.Scoped<IStorageInitialization, ApplicationContext>());

        return services;
    }
    
    public static IServiceCollection AddDatabaseInMemory(this IServiceCollection services)
    {
        services.TryAdd(ServiceDescriptor.Singleton<IContextOptionsFactory, SqliteInMemoryContextOptionsFactory>());
        services.TryAdd(ServiceDescriptor.Scoped<IStorageReader, ApplicationContext>());
        services.TryAdd(ServiceDescriptor.Scoped<IStorageWriter, ApplicationContext>());
        services.TryAdd(ServiceDescriptor.Scoped<IStorageInitialization, ApplicationContext>());

        return services;
    }
}