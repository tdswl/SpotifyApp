using SpotifyApp.Api.Contracts.Users.Responses;
using Flurl.Http;

namespace SpotifyApp.Api.Client.Users;

internal class UsersClient : IUsersClient
{
    public Task<GetCurrentUserProfileResponse> GetCurrentUserProfile(string accessToken, CancellationToken cancellationToken)
    {
        return "https://api.spotify.com/v1/me"
            .WithOAuthBearerToken(accessToken)
            .GetJsonAsync<GetCurrentUserProfileResponse>(cancellationToken);
    }
}