using Flurl;
using Flurl.Http;
using SpotifyApp.Api.Contracts.Tracks.Requests;
using SpotifyApp.Api.Contracts.Tracks.Responses;

namespace SpotifyApp.Api.Client.Tracks;

internal class TracksClient : ITracksClient
{
    public Task<GetTrackResponse> GetTrack(GetTrackRequest request, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        var query = "https://api.spotify.com/v1/tracks/"
            .AppendPathSegment(request.Id);

        if (request.Market != null)
        {
            query.SetQueryParam("market", request.Market);
        }
        
        return query
            .WithOAuthBearerToken(accessToken)
            .GetJsonAsync<GetTrackResponse>(cancellationToken);
    }

    public Task<GetSeveralTracksResponse> GetSeveralTracks(GetSeveralTracksRequest request, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        var query = "https://api.spotify.com/v1/tracks"
            .SetQueryParam("ids", request.Ids);

        if (request.Market != null)
        {
            query.SetQueryParam("market", request.Market);
        }
        
        return query
            .WithOAuthBearerToken(accessToken)
            .GetJsonAsync<GetSeveralTracksResponse>(cancellationToken);
    }
}