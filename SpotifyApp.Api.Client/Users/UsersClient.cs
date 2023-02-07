using Flurl;
using SpotifyApp.Api.Contracts.Users.Responses;
using Flurl.Http;
using SpotifyApp.Api.Contracts.Users.Enums;

namespace SpotifyApp.Api.Client.Users;

internal class UsersClient : IUsersClient
{
    public Task<GetCurrentUserProfileResponse> GetCurrentUserProfile(string accessToken, 
        CancellationToken cancellationToken)
    {
        return "https://api.spotify.com/v1/me"
            .WithOAuthBearerToken(accessToken)
            .GetJsonAsync<GetCurrentUserProfileResponse>(cancellationToken);
    }

    public Task<GetUsersTopItemsResponse> GetUsersTopItems(ItemsType type, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return "https://api.spotify.com/v1/me/top/"
            .AppendPathSegment(type.ToString().ToLower())
            .WithOAuthBearerToken(accessToken)
            .GetJsonAsync<GetUsersTopItemsResponse>(cancellationToken);
    }
}