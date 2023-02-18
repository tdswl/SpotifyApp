using Flurl.Http;
using SpotifyApp.Api.Client.Extensions;
using SpotifyApp.Api.Contracts.Artists.Requests;
using SpotifyApp.Api.Contracts.Artists.Responses;
using SpotifyApp.Api.Contracts.Base.Models;

namespace SpotifyApp.Api.Client.Artists;

internal class ArtistsClient : IArtistsClient
{
    public Task<ItemModelApi> GetArtist(string id, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return ArtistsRoutes.GetArtist
            .WithOAuthBearerToken(accessToken)
            .AppendPathSegment(id)
            .GetJsonAsync<ItemModelApi>(cancellationToken);
    }

    public Task<GetArtistsAlbumsResponse> GetArtistsAlbums(string artistId, 
        GetArtistsAlbumsRequest request, 
        string accessToken,
        CancellationToken cancellationToken)
    {
        var query = string.Format(ArtistsRoutes.GetArtistsAlbums, artistId)
            .WithOAuthBearerToken(accessToken)
            .SetPagedQueryParams(request);
        
        if (request.Market != null)
        {
            query = query.SetQueryParam("market", request.Market);
        }
        
        if (request.IncludeGroups != null && request.IncludeGroups.Any())
        {
            query = query.SetQueryParam("include_groups",
                string.Join(",", request.IncludeGroups.Select(a => a.ToString().ToLowerInvariant())));
        }
        
        return query.GetJsonAsync<GetArtistsAlbumsResponse>(cancellationToken);
    }
}