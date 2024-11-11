using IdentityModel.OidcClient.Browser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SpotifyApp.Browser.Browser;
using SpotifyApp.Storage.Sqlite.DI;

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
        services.AddSqliteInMemoryStorage();

        return services;
    }
}