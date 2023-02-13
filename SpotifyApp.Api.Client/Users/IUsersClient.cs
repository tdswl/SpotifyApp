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
    /// Get public profile information about a Spotify user.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/get-users-profile
    /// </summary>
    Task<GetUsersProfileResponse> GetUsersProfile(string userId,
        string accessToken,
        CancellationToken cancellationToken);

    /// <summary>
    /// Add the current user as a follower of a playlist.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/follow-playlist
    /// </summary>
    Task FollowPlaylist(string playlistId,
        FollowPlaylistRequest request,
        string accessToken,
        CancellationToken cancellationToken);
    
    /// <summary>
    /// Remove the current user as a follower of a playlist.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/unfollow-playlist
    /// </summary>
    Task UnfollowPlaylist(string playlistId,
        string accessToken,
        CancellationToken cancellationToken);
    
    /// <summary>
    /// Get the current user's followed artists.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/get-followed
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

    /// <summary>
    /// Add the current user as a follower of one or more artists or other Spotify users.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/follow-artists-users
    /// </summary>
    Task FollowArtistsOrUsers(FollowArtistsOrUsersRequest request,
        string accessToken,
        CancellationToken cancellationToken);
    
    /// <summary>
    /// Remove the current user as a follower of one or more artists or other Spotify users.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/unfollow-artists-users
    /// </summary>
    Task UnfollowArtistsOrUsers(FollowArtistsOrUsersRequest request,
        string accessToken,
        CancellationToken cancellationToken);

    /// <summary>
    /// Check to see if the current user is following one or more artists or other Spotify users.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/check-current-user-follows
    /// </summary>
    Task<IReadOnlyList<bool>> CheckIfUserFollowsArtistsOrUsers(FollowArtistsOrUsersRequest request,
        string accessToken,
        CancellationToken cancellationToken);

    /// <summary>
    /// Check to see if one or more Spotify users are following a specified playlist.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/check-if-user-follows-playlist
    /// </summary>
    Task<IReadOnlyList<bool>> CheckIfUsersFollowPlaylist(string playlistId,
        string ids,
        string accessToken,
        CancellationToken cancellationToken);
}