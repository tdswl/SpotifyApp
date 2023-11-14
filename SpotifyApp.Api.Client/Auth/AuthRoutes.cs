namespace SpotifyApp.Api.Client.Auth;

internal static class AuthRoutes
{
    public const string IssuerName = "https://accounts.spotify.com/";

    public const string AuthorizeEndpoint = "https://accounts.spotify.com/authorize";

    public const string TokenEndpoint = "https://accounts.spotify.com/api/token";
    
    public const string GetCurrentUserProfile = "https://api.spotify.com/v1/me";
}