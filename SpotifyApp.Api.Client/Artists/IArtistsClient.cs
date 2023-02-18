using Flurl.Http;
using SpotifyApp.Api.Contracts.Artists.Requests;
using SpotifyApp.Api.Contracts.Artists.Responses;
using SpotifyApp.Api.Contracts.Base.Models;

namespace SpotifyApp.Api.Client.Artists;

public interface IArtistsClient
{
    /// <summary>
    /// Get Spotify catalog information for a single artist identified by their unique Spotify ID.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/get-an-artist
    /// </summary>
    /// <exception cref="FlurlHttpException">
    /// 401 - Bad or expired token. This can happen if the user revoked a token or the
    /// access token has expired. You should re-authenticate the user.
    ///
    /// 403 - Bad OAuth request (wrong consumer key, bad nonce, expired timestamp...).
    /// Unfortunately, re-authenticating the user won't help here.
    ///
    /// 429 - The app has exceeded its rate limits.
    /// </exception>
    Task<ItemModelApi> GetArtist(string id,
        string accessToken,
        CancellationToken cancellationToken);

    /// <summary>
    /// Get Spotify catalog information about an artist's albums.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/get-an-artists-albums
    /// </summary>
    Task<GetArtistsAlbumsResponse> GetArtistsAlbums(string artistId,
        GetArtistsAlbumsRequest request,
        string accessToken,
        CancellationToken cancellationToken);
}