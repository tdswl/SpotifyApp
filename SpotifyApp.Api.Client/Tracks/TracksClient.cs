using Flurl.Http;
using SpotifyApp.Api.Contracts.Tracks.Models;
using SpotifyApp.Api.Contracts.Tracks.Requests;
using SpotifyApp.Api.Contracts.Tracks.Responses;

namespace SpotifyApp.Api.Client.Tracks;

internal class TracksClient : ITracksClient
{
    Task<GetTrackResponse> ITracksClient.GetTrack(GetTrackRequest request, 
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

    Task<GetSeveralTracksResponse> ITracksClient.GetSeveralTracks(GetSeveralTracksRequest request, 
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

    Task<GetUsersSavedTracksResponse> ITracksClient.GetUsersSavedTracks(GetUsersSavedTracksRequest request,
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

    public Task SaveTracksForCurrentUser(string ids, string accessToken, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveUsersSavedTracks(string ids, string accessToken, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<bool>> CheckUsersSavedTracks(string ids, string accessToken, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<GetTracksAudioFeaturesResponse> GetTracksAudioFeatures(string ids, string accessToken, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<AudioFeaturesModel> GetTrackAudioFeatures(string id, string accessToken, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<GetTracksAudioAnalysisResponse> GetTracksAudioAnalysis(string id, string accessToken, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<GetRecommendationsResponse> GetRecommendations(GetRecommendationsRequest request, string accessToken, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}