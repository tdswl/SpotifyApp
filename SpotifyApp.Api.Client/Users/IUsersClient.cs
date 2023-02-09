using Flurl.Http;
using SpotifyApp.Api.Contracts.Users.Requests;
using SpotifyApp.Api.Contracts.Users.Responses;

namespace SpotifyApp.Api.Client.Users;

/// <summary>
/// Api for getting information for User from Spotify
/// </summary>
public interface IUsersClient
{
    /// <summary>
    /// Get detailed profile information about the current user (including the current user's username).
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/get-current-users-profile
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
    Task<GetCurrentUserProfileResponse> GetCurrentUserProfile(string accessToken,
        CancellationToken cancellationToken);

    /// <summary>
    /// Get the current user's top artists or tracks based on calculated affinity.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/get-users-top-artists-and-tracks
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
    Task<GetUsersTopItemsResponse> GetUsersTopItems(GetUsersTopItemsRequest request, 
        string accessToken,
        CancellationToken cancellationToken);

    /// <summary>
    /// Get the current user's followed artists.
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
    Task<GetFollowedArtistsResponse> GetFollowedArtists(GetFollowedArtistsRequest request,
        string accessToken,
        CancellationToken cancellationToken);
}