using SpotifyApp.Api.Contracts.Auth;
using SpotifyApp.Shared.Constants;

namespace SpotifyApp.Shared.Configurations;

public sealed class OidcConfiguration : IOidcConfiguration
{
    public string ClientId => ApplicationSettings.ClientId;
    public string RedirectUri => ApplicationSettings.RedirectUri;
    public string Scope  => ApplicationSettings.SpotifyScopes;
}