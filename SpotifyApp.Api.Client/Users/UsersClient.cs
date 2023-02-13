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
            .WithOAuthBearerToken(accessToken)
            .AppendPathSegment($"{request.Type.ToString().ToLower()}s");

        if (request.Limit != null)
        {
            query = query.SetQueryParam("limit", request.Limit);
        }
        
        if (request.Offset != null)
        {
            query = query.SetQueryParam("offset", request.Offset);
        }
        
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
        return "https://api.spotify.com/v1/users"
            .WithOAuthBearerToken(accessToken)
            .AppendPathSegment(userId)
            .GetJsonAsync<GetUsersProfileResponse>(cancellationToken);
    }

    Task IUsersClient.FollowPlaylist(string playlistId, 
        FollowPlaylistRequest request,
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return "https://api.spotify.com/v1/playlists"
            .WithOAuthBearerToken(accessToken)
            .AppendPathSegment(playlistId)
            .AppendPathSegment("followers")
            .PutJsonAsync(request, cancellationToken: cancellationToken);
    }

    Task IUsersClient.UnfollowPlaylist(string playlistId, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return "https://api.spotify.com/v1/playlists"
            .WithOAuthBearerToken(accessToken)
            .AppendPathSegment(playlistId)
            .AppendPathSegment("followers")
            .DeleteAsync(cancellationToken: cancellationToken);
    }
    
    Task<GetFollowedArtistsResponse> IUsersClient.GetFollowedArtists(GetFollowedArtistsRequest request, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        var query = "https://api.spotify.com/v1/me/following"
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
        return "https://api.spotify.com/v1/me/following"
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("type", request.Type.ToString().ToLower())
            .SetQueryParam("ids", request.Ids)
            .PutAsync(cancellationToken: cancellationToken);
    }

    Task IUsersClient.UnfollowArtistsOrUsers(FollowArtistsOrUsersRequest request, 
        string accessToken,
        CancellationToken cancellationToken)
    {
        return "https://api.spotify.com/v1/me/following"
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("type", request.Type.ToString().ToLower())
            .SetQueryParam("ids", request.Ids)
            .DeleteAsync(cancellationToken);
    }

    Task<IReadOnlyList<bool>> IUsersClient.CheckIfUserFollowsArtistsOrUsers(FollowArtistsOrUsersRequest request,
        string accessToken,
        CancellationToken cancellationToken)
    {
        return "https://api.spotify.com/v1/me/following/contains"
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
        return "https://api.spotify.com/v1/playlists"
            .WithOAuthBearerToken(accessToken)
            .AppendPathSegment(playlistId)
            .AppendPathSegment(playlistId)
            .SetQueryParam("followers/contains")
            .GetJsonAsync<IReadOnlyList<bool>>(cancellationToken);
    }
}