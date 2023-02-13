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
            .WithOAuthBearerToken(accessToken)
            .AppendPathSegment(request.Id);

        if (request.Market != null)
        {
            query = query.SetQueryParam("market", request.Market);
        }
        
        return query.GetJsonAsync<GetTrackResponse>(cancellationToken);
    }

    public Task<GetSeveralTracksResponse> GetSeveralTracks(GetSeveralTracksRequest request, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        var query = "https://api.spotify.com/v1/tracks"
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("ids", request.Ids);

        if (request.Market != null)
        {
            query = query.SetQueryParam("market", request.Market);
        }
        
        return query.GetJsonAsync<GetSeveralTracksResponse>(cancellationToken);
    }

    public Task<GetUsersSavedTracksResponse> GetUsersSavedTracks(GetUsersSavedTracksRequest request,
        string accessToken, 
        CancellationToken cancellationToken)
    {
        var query = "https://api.spotify.com/v1/me/tracks"
            .WithOAuthBearerToken(accessToken);

        if (request.Limit != null) 
        {
            query = query.SetQueryParam("limit", request.Limit);
        }
        
        if (request.Offset != null)
        {
            query = query.SetQueryParam("offset", request.Offset);
        }
        
        if (request.Market != null)
        {
            query = query.SetQueryParam("market", request.Market);
        }
        
        return query.GetJsonAsync<GetUsersSavedTracksResponse>(cancellationToken);
    }
}