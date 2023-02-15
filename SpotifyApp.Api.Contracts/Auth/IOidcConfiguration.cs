namespace SpotifyApp.Api.Contracts.Auth;

public interface IOidcConfiguration
{
    string ClientId { get; }

    string RedirectUri { get; }

    string Scope { get; }
}