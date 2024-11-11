using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SpotifyApp.Api.Client.Auth;
using SpotifyApp.Services.DI;
using SpotifyApp.Shared.AutoMapper;
using SpotifyApp.Shared.Configurations;
using SpotifyApp.Shared.Services;
using SpotifyApp.Shared.ViewModels;

namespace SpotifyApp.Shared.DI;

/// <summary>
/// Extensions with service collections
/// </summary>
public static class AppServiceCollectionExtensions
{
    public static IServiceCollection AddAppDi(this IServiceCollection services)
    {
        return services
            .AddLogging(builder => builder.AddSerilog())
            .AddMemoryCache()
            .AddAutomapper()
            .AddServices()
            
            // Models shared across component - register as singleton
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<NavigateViewModel>()
           
            // App services
            .AddSingleton<IOidcConfiguration, OidcConfiguration>()
            .AddSingleton<INavigationService, NavigationService>();
    }

    private static IServiceCollection AddAutomapper(this IServiceCollection services)
    {
        services = services.AddAutoMapper(AutoMapperConfig);

        return services;
    }

    private static void AutoMapperConfig(IMapperConfigurationExpression cfg)
    {
        cfg.AddProfile<ApplicationMapProfile>();
    }
}