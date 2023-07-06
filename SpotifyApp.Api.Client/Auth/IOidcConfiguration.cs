namespace SpotifyApp.Api.Client.Auth;

public interface IOidcConfiguration
{   
    string ClientId { get; }

    string RedirectUri { get; }

    /// <summary>
    /// https://developer.spotify.com/dccumentation/general/guides/authorization/scopes
    /// </summary>
    string Scope { get; }
}