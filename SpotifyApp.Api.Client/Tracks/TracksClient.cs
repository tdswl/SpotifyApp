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
        var query = TracksRoutes.GetTrack
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
        var query = TracksRoutes.GetTrack
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
        var query = TracksRoutes.GetUsersSavedTracks
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

    Task ITracksClient.SaveTracksForCurrentUser(string ids, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return TracksRoutes.GetUsersSavedTracks
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("ids", ids)
            .PutAsync(cancellationToken: cancellationToken);
    }

    Task ITracksClient.RemoveUsersSavedTracks(string ids, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return TracksRoutes.GetUsersSavedTracks
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("ids", ids)
            .DeleteAsync(cancellationToken: cancellationToken);
    }

    Task<IReadOnlyList<bool>> ITracksClient.CheckUsersSavedTracks(string ids, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return TracksRoutes.CheckUsersSavedTracks
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("ids", ids)
            .GetJsonAsync<IReadOnlyList<bool>>(cancellationToken: cancellationToken);
    }

    Task<GetTracksAudioFeaturesResponse> ITracksClient.GetTracksAudioFeatures(string ids, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return TracksRoutes.GetTracksAudioFeatures
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("ids", ids)
            .GetJsonAsync<GetTracksAudioFeaturesResponse>(cancellationToken: cancellationToken);
    }

    Task<AudioFeaturesModel> ITracksClient.GetTrackAudioFeatures(string id, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return TracksRoutes.GetTracksAudioFeatures
            .WithOAuthBearerToken(accessToken)
            .AppendPathSegment(id)
            .GetJsonAsync<AudioFeaturesModel>(cancellationToken: cancellationToken);
    }

    Task<GetTracksAudioAnalysisResponse> ITracksClient.GetTracksAudioAnalysis(string id, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return TracksRoutes.GetTracksAudioAnalysis
            .WithOAuthBearerToken(accessToken)
            .AppendPathSegment(id)
            .GetJsonAsync<GetTracksAudioAnalysisResponse>(cancellationToken: cancellationToken);
    }

    Task<GetRecommendationsResponse> ITracksClient.GetRecommendations(GetRecommendationsRequest request, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        var query = TracksRoutes.GetRecommendations
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("seed_artists", request.SeedArtists)
            .SetQueryParam("seed_genres", request.SeedGenres)
            .SetQueryParam("seed_tracks", request.SeedTracks);
        
        // TODO: other query params
        
        return query.GetJsonAsync<GetRecommendationsResponse>(cancellationToken);
    }
}