namespace SpotifyApp.Api.Client.Users;

public static class UserRoutes
{
    public const string GetCurrentUserProfile = "https://api.spotify.com/v1/me";

    public const string GetUsersTopItems = "https://api.spotify.com/v1/me/top/";
    
    public const string GetUsersProfile = "https://api.spotify.com/v1/users";

    public const string FollowPlaylist = "https://api.spotify.com/v1/playlists/{0}/followers";

    public const string GetFollowedArtists = "https://api.spotify.com/v1/me/following";

    public const string FollowArtistsOrUsers = "https://api.spotify.com/v1/me/following";

    public const string CheckIfUserFollowsArtistsOrUsers = "https://api.spotify.com/v1/me/following/contains";

    public const string CheckIfUsersFollowPlaylist = "https://api.spotify.com/v1/playlists/{0}/followers/contains";
}