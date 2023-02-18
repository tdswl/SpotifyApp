using Flurl.Http;
using SpotifyApp.Api.Client.Extensions;
using SpotifyApp.Api.Contracts.Albums.Requests;
using SpotifyApp.Api.Contracts.Albums.Responses;
using SpotifyApp.Api.Contracts.Base.Requests;

namespace SpotifyApp.Api.Client.Albums;

internal class AlbumsClient : IAlbumsClient
{
    public Task<GetAlbumResponse> GetAlbum(IdRequest request, 
        string accessToken, 
        CancellationToken token)
    {
        return AlbumsRoutes.GetAlbum
            .WithOAuthBearerToken(accessToken)
            .AppendPathSegment(request.Id)
            .GetJsonAsync<GetAlbumResponse>(token);
    }

    public Task<GetSeveralAlbumsResponse> GetSeveralAlbums(IdsRequest request, 
        string accessToken, 
        CancellationToken token)
    {
        return AlbumsRoutes.GetAlbum
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("ids", request.Ids)
            .GetJsonAsync<GetSeveralAlbumsResponse>(token);
    }

    public Task<GetAlbumTracksResponse> GetAlbumTracks(GetAlbumTracksRequest request,
        string accessToken,
        CancellationToken token)
    {
        return string.Format(AlbumsRoutes.GetAlbumTracks, request.Id)
            .WithOAuthBearerToken(accessToken)
            .SetMarketParams(request)
            .GetJsonAsync<GetAlbumTracksResponse>(token);
    }

    public Task<GetUsersSavedAlbumsResponse> GetUsersSavedAlbums(GetUsersSavedAlbumsRequest request, string accessToken, CancellationToken token)
    {
        return AlbumsRoutes.GetUsersSavedAlbums
            .WithOAuthBearerToken(accessToken)
            .SetPagedQueryParams(request)
            .SetMarketParams(request)
            .GetJsonAsync<GetUsersSavedAlbumsResponse>(token);
    }

    public Task SaveAlbumsForCurrentUser(string ids, string accessToken, CancellationToken token)
    {
        return AlbumsRoutes.GetUsersSavedAlbums
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("ids", ids)
            .PutAsync(cancellationToken: token);
    }

    public Task RemoveUsersSavedAlbums(string ids, string accessToken, CancellationToken token)
    {
        return AlbumsRoutes.GetUsersSavedAlbums
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("ids", ids)
            .DeleteAsync(cancellationToken: token);
    }

    public Task<IReadOnlyList<bool>> CheckUsersSavedAlbums(string ids, string accessToken, CancellationToken token)
    {
        return AlbumsRoutes.CheckUsersSavedAlbums
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("ids", ids)
            .GetJsonAsync<IReadOnlyList<bool>>(cancellationToken: token);
    }

    public Task<GetNewReleasesResponse> GetNewReleases(GetNewReleasesRequest request, string accessToken, CancellationToken token)
    {
        var query = AlbumsRoutes.GetNewReleases
            .WithOAuthBearerToken(accessToken)
            .SetPagedQueryParams(request);
        
        if (request.Country != null)
        {
            query = query.SetQueryParam("country", request.Country);
        }
        
        return query.GetJsonAsync<GetNewReleasesResponse>(cancellationToken: token);
    }
}