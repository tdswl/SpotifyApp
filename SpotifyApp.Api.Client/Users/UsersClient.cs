using SpotifyApp.Api.Contracts.Users.Responses;
using Flurl.Http;
using SpotifyApp.Api.Client.Extensions;
using SpotifyApp.Api.Contracts.Users.Requests;

namespace SpotifyApp.Api.Client.Users;

internal class UsersClient : IUsersClient
{
    Task<GetCurrentUserProfileResponse> IUsersClient.GetCurrentUserProfile(string accessToken, 
        CancellationToken cancellationToken)
    {
        return UserRoutes.GetCurrentUserProfile
            .WithOAuthBearerToken(accessToken)
            .GetJsonAsync<GetCurrentUserProfileResponse>(cancellationToken);
    }

    Task<GetUsersTopItemsResponse> IUsersClient.GetUsersTopItems(GetUsersTopItemsRequest request, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        var query = UserRoutes.GetUsersTopItems
            .WithOAuthBearerToken(accessToken)
            .AppendPathSegment($"{request.Type.ToString().ToLower()}s")
            .SetPagedQueryParams(request);
        
        if (request.TimeRange != null)
        {
            query = query.SetQueryParam("time_range", request.TimeRange);
        }
        
        return query.GetJsonAsync<GetUsersTopItemsResponse>(cancellationToken);
    }

    Task<GetUsersProfileResponse> IUsersClient.GetUsersProfile(string userId, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return UserRoutes.GetUsersProfile
            .WithOAuthBearerToken(accessToken)
            .AppendPathSegment(userId)
            .GetJsonAsync<GetUsersProfileResponse>(cancellationToken);
    }

    Task IUsersClient.FollowPlaylist(string playlistId, 
        FollowPlaylistRequest request,
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return string.Format(UserRoutes.FollowPlaylist, playlistId)
            .WithOAuthBearerToken(accessToken)
            .PutJsonAsync(request, cancellationToken: cancellationToken);
    }

    Task IUsersClient.UnfollowPlaylist(string playlistId, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return string.Format(UserRoutes.FollowPlaylist, playlistId)
            .WithOAuthBearerToken(accessToken)
            .DeleteAsync(cancellationToken: cancellationToken);
    }
    
    Task<GetFollowedArtistsResponse> IUsersClient.GetFollowedArtists(GetFollowedArtistsRequest request, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        var query = UserRoutes.GetFollowedArtists
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("type", request.Type.ToString().ToLower());

        if (request.Limit != null)
        {
            query = query.SetQueryParam("limit", request.Limit);
        }
        
        if (request.After != null)
        {
            query = query.SetQueryParam("after", request.After);
        }
        
        return query.GetJsonAsync<GetFollowedArtistsResponse>(cancellationToken);
    }

    Task IUsersClient.FollowArtistsOrUsers(FollowArtistsOrUsersRequest request, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return UserRoutes.FollowArtistsOrUsers
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("type", request.Type.ToString().ToLower())
            .SetQueryParam("ids", request.Ids)
            .PutAsync(cancellationToken: cancellationToken);
    }

    Task IUsersClient.UnfollowArtistsOrUsers(FollowArtistsOrUsersRequest request, 
        string accessToken,
        CancellationToken cancellationToken)
    {
        return UserRoutes.FollowArtistsOrUsers
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("type", request.Type.ToString().ToLower())
            .SetQueryParam("ids", request.Ids)
            .DeleteAsync(cancellationToken);
    }

    Task<IReadOnlyList<bool>> IUsersClient.CheckIfUserFollowsArtistsOrUsers(FollowArtistsOrUsersRequest request,
        string accessToken,
        CancellationToken cancellationToken)
    {
        return UserRoutes.CheckIfUserFollowsArtistsOrUsers
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("type", request.Type.ToString().ToLower())
            .SetQueryParam("ids", request.Ids)
            .GetJsonAsync<IReadOnlyList<bool>>(cancellationToken);
    }

    Task<IReadOnlyList<bool>> IUsersClient.CheckIfUsersFollowPlaylist(string playlistId, 
        string ids, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return string.Format(UserRoutes.CheckIfUsersFollowPlaylist, playlistId)
            .WithOAuthBearerToken(accessToken)
            .GetJsonAsync<IReadOnlyList<bool>>(cancellationToken);
    }
}