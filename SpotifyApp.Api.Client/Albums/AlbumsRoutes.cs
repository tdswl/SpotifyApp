namespace SpotifyApp.Api.Client.Albums;

public static class AlbumsRoutes
{
    public const string GetAlbum = "https://api.spotify.com/v1/albums";
    
    public const string GetAlbumTracks = "https://api.spotify.com/v1/albums/{0}/tracks";

    public const string GetUsersSavedAlbums = "https://api.spotify.com/v1/me/albums";

    public const string CheckUsersSavedAlbums = "https://api.spotify.com/v1/me/albums/contains";

    public const string GetNewReleases = "https://api.spotify.com/v1/browse/new-releases";
}