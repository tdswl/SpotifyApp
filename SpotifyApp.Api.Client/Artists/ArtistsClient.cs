using Flurl.Http;
using SpotifyApp.Api.Contracts.Base.Models;

namespace SpotifyApp.Api.Client.Artists;

internal class ArtistsClient : IArtistsClient
{
    public Task<ItemModelApi> GetArtist(string id, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return "https://api.spotify.com/v1/artists/"
            .WithOAuthBearerToken(accessToken)
            .AppendPathSegment(id)
            .GetJsonAsync<ItemModelApi>(cancellationToken);
    }
}