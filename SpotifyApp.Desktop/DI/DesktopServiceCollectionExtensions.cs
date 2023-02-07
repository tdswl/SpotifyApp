using IdentityModel.OidcClient.Browser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SpotifyApp.Desktop.Browser;

namespace SpotifyApp.Desktop.DI;

/// <summary>
/// DependencyInjection extension
/// </summary>
public static class DesktopServiceCollectionExtensions
{
    /// <summary>
    /// Add browser for oauth2
    /// </summary>
    public static IServiceCollection AddDesktopBrowser(this IServiceCollection services)
    {
        services.TryAdd(ServiceDescriptor.Scoped<IBrowser, SystemBrowser>());

        return services;
    }
}