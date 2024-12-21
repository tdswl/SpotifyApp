using IdentityModel.OidcClient.Browser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SpotifyApp.Browser.Browser;
using SpotifyApp.Browser.Repositories;
using SpotifyApp.Browser.Services;
using SpotifyApp.Browser.Storage;
using SpotifyApp.Repositories.Contracts;
using SpotifyApp.Services.Contracts;
using SpotifyApp.Storage.Contracts.Interfaces;

namespace SpotifyApp.Browser.DI;

/// <summary>
/// DependencyInjection extension
/// </summary>
public static class BrowserServiceCollectionExtensions
{
    /// <summary>
    /// Add browser for oauth2
    /// </summary>
    public static IServiceCollection AddBrowser(this IServiceCollection services)
    {
        services.TryAdd(ServiceDescriptor.Scoped<IBrowser, SystemBrowser>());
        services.TryAdd(ServiceDescriptor.Singleton<IImageCache, ImageCache>());
        services.TryAdd(ServiceDescriptor.Scoped<IOAuthTokenReadRepository, OAuthTokenLocalStorageReadRepository>());
        services.TryAdd(ServiceDescriptor.Scoped<IOAuthTokenWriteRepository, OAuthTokenLocalStorageWriteRepository>());
        services.TryAdd(ServiceDescriptor.Scoped<IStorageInitialization, WasmStorageInitialization>());

        return services;
    }
}