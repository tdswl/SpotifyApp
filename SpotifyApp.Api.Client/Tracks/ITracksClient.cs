using Flurl.Http;
using SpotifyApp.Api.Contracts.Tracks.Requests;
using SpotifyApp.Api.Contracts.Tracks.Responses;

namespace SpotifyApp.Api.Client.Tracks;

public interface ITracksClient
{
    /// <summary>
    /// Get Spotify catalog information for a single track identified by its unique Spotify ID.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/get-track
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
    Task<GetTrackResponse> GetTrack(GetTrackRequest request, 
        string accessToken,
        CancellationToken cancellationToken);
    
    /// <summary>
    /// Get Spotify catalog information for multiple tracks based on their Spotify IDs.
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
    Task<GetSeveralTracksResponse> GetSeveralTracks(GetSeveralTracksRequest request, 
        string accessToken,
        CancellationToken cancellationToken);
}