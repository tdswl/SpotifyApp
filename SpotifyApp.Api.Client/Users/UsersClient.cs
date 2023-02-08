using Flurl;
using SpotifyApp.Api.Contracts.Users.Responses;
using Flurl.Http;
using SpotifyApp.Api.Contracts.Users.Requests;

namespace SpotifyApp.Api.Client.Users;

internal class UsersClient : IUsersClient
{
    Task<GetCurrentUserProfileResponse> IUsersClient.GetCurrentUserProfile(string accessToken, 
        CancellationToken cancellationToken)
    {
        return "https://api.spotify.com/v1/me"
            .WithOAuthBearerToken(accessToken)
            .GetJsonAsync<GetCurrentUserProfileResponse>(cancellationToken);
    }

    Task<GetUsersTopItemsResponse> IUsersClient.GetUsersTopItems(GetUsersTopItemsRequest request, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        var query = "https://api.spotify.com/v1/me/top/"
            .AppendPathSegment(request.Type.ToString().ToLower());

        if (request.Limit != null)
        {
            query.SetQueryParam("limit", request.Limit);
        }
        
        if (request.Offset != null)
        {
            query.SetQueryParam("offset", request.Offset);
        }
        
        if (request.Limit != null)
        {
            query.SetQueryParam("time_range", request.TimeRange);
        }
        
        return query
            .WithOAuthBearerToken(accessToken)
            .GetJsonAsync<GetUsersTopItemsResponse>(cancellationToken);
    }
}