using SpotifyApp.Api.Client.Auth;

namespace SpotifyApp.Shared.Configurations;

public sealed class OidcConfiguration : IOidcConfiguration
{
    public string ClientId => "";
    public string RedirectUri => "http://127.0.0.1:7890";

    public string Scope => string.Join(" ", new List<string>
    {
        // read
        "user-read-private",
        "user-read-email",
        "user-library-read",
        "user-top-read",
        "user-follow-read",
        "playlist-read-collaborative",
        "playlist-read-private",
        "user-read-currently-playing",
        "user-read-recently-played",
        "user-read-playback-state",
        "user-read-playback-position"
    });
}