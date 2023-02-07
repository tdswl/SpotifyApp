using SpotifyApp.Api.Contracts.Users.Enums;
using SpotifyApp.Api.Contracts.Users.Responses;

namespace SpotifyApp.Api.Client.Users;

public interface IUsersClient
{
    /// <summary>
    /// Get detailed profile information about the current user (including the current user's username).
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/get-current-users-profile
    /// </summary>
    Task<GetCurrentUserProfileResponse> GetCurrentUserProfile(string accessToken,
        CancellationToken cancellationToken);

    /// <summary>
    /// Get the current user's top artists or tracks based on calculated affinity.
    /// https://developer.spotify.com/documentation/web-api/reference/#/operations/get-users-top-artists-and-tracks
    /// </summary>
    Task<GetUsersTopItemsResponse> GetUsersTopItems(ItemsType type, string accessToken,
        CancellationToken cancellationToken);
}