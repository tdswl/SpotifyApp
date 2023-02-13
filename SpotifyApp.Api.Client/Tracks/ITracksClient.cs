using Flurl.Http;
using SpotifyApp.Api.Contracts.Tracks.Models;
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
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/get-several-tracks
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

    /// <summary>
    /// Get a list of the songs saved in the current Spotify user's 'Your Music' library.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/get-users-saved-tracks
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
    Task<GetUsersSavedTracksResponse> GetUsersSavedTracks(GetUsersSavedTracksRequest request,
        string accessToken,
        CancellationToken cancellationToken);

    /// <summary>
    /// Save one or more tracks to the current user's 'Your Music' library.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/save-tracks-user
    /// </summary>
    Task SaveTracksForCurrentUser(string ids,
        string accessToken,
        CancellationToken cancellationToken);
    
    /// <summary>
    /// Remove one or more tracks from the current user's 'Your Music' library.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/remove-tracks-user
    /// </summary>
    Task RemoveUsersSavedTracks(string ids,
        string accessToken,
        CancellationToken cancellationToken);
    
    /// <summary>
    /// Check if one or more tracks is already saved in the current Spotify user's 'Your Music' library.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/check-users-saved-tracks
    /// </summary>
    Task<IReadOnlyList<bool>> CheckUsersSavedTracks(string ids,
        string accessToken,
        CancellationToken cancellationToken);
    
    /// <summary>
    /// Get audio features for multiple tracks based on their Spotify IDs.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/check-users-saved-tracks
    /// </summary>
    Task<GetTracksAudioFeaturesResponse> GetTracksAudioFeatures(string ids,
        string accessToken,
        CancellationToken cancellationToken);
    
    /// <summary>
    /// Get audio feature information for a single track identified by its unique Spotify ID.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/get-audio-features
    /// </summary>
    Task<AudioFeaturesModel> GetTrackAudioFeatures(string id,
        string accessToken,
        CancellationToken cancellationToken);
    
    /// <summary>
    /// Get a low-level audio analysis for a track in the Spotify catalog.
    /// The audio analysis describes the track’s structure and musical content,
    /// including rhythm, pitch, and timbre.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/get-audio-analysis
    /// </summary>
    Task<GetTracksAudioAnalysisResponse> GetTracksAudioAnalysis(string id,
        string accessToken,
        CancellationToken cancellationToken);
    
    /// <summary>
    /// Recommendations are generated based on the available information for a given seed entity and matched
    /// against similar artists and tracks. If there is sufficient information about the provided seeds, a list
    /// of tracks will be returned together with pool size details.
    /// For artists and tracks that are very new or obscure there might not be enough data to generate a list of tracks.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/get-recommendations
    /// </summary>
    Task<GetRecommendationsResponse> GetRecommendations(GetRecommendationsRequest request,
        string accessToken,
        CancellationToken cancellationToken);
}