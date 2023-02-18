using Flurl.Http;
using SpotifyApp.Api.Contracts.Albums.Requests;
using SpotifyApp.Api.Contracts.Albums.Responses;
using SpotifyApp.Api.Contracts.Base.Requests;

namespace SpotifyApp.Api.Client.Albums;

internal class AlbumsClient : IAlbumsClient
{
    public Task<GetAlbumResponse> GetAlbum(IdRequest request, string accessToken, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<GetSeveralAlbumsResponse> GetSeveralAlbums(IdsRequest request, string accessToken, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<GetAlbumTracksResponse> GetAlbumTracks(GetAlbumTracksRequest request,
        string accessToken,
        CancellationToken token)
    {
        var query = string.Format(AlbumsRoutes.GetAlbumTracks, request.Id)
            .WithOAuthBearerToken(accessToken);

        if (request.Market != null)
        {
            query = query.SetQueryParam("market", request.Market);
        }
        
        return query.GetJsonAsync<GetAlbumTracksResponse>(token);
    }

    public Task<GetUsersSavedAlbumsResponse> GetUsersSavedAlbums(GetUsersSavedAlbumsRequest request, string accessToken, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task SaveAlbumsForCurrentUser(string ids, string accessToken, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task RemoveUsersSavedAlbums(string ids, string accessToken, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<bool>> CheckUsersSavedAlbums(string ids, string accessToken, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<GetNewReleasesResponse> GetNewReleases(GetNewReleasesRequest request, string accessToken, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}