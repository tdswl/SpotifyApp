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

    Task ITracksClient.SaveTracksForCurrentUser(string ids, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return "https://api.spotify.com/v1/me/tracks"
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("ids", ids)
            .PutAsync(cancellationToken: cancellationToken);
    }

    Task ITracksClient.RemoveUsersSavedTracks(string ids, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return "https://api.spotify.com/v1/me/tracks"
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("ids", ids)
            .DeleteAsync(cancellationToken: cancellationToken);
    }

    Task<IReadOnlyList<bool>> ITracksClient.CheckUsersSavedTracks(string ids, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return "https://api.spotify.com/v1/me/tracks/contains"
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("ids", ids)
            .GetJsonAsync<IReadOnlyList<bool>>(cancellationToken: cancellationToken);
    }

    Task<GetTracksAudioFeaturesResponse> ITracksClient.GetTracksAudioFeatures(string ids, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return "https://api.spotify.com/v1/audio-features"
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("ids", ids)
            .GetJsonAsync<GetTracksAudioFeaturesResponse>(cancellationToken: cancellationToken);
    }

    Task<AudioFeaturesModel> ITracksClient.GetTrackAudioFeatures(string id, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return "https://api.spotify.com/v1/audio-features"
            .WithOAuthBearerToken(accessToken)
            .AppendPathSegment(id)
            .GetJsonAsync<AudioFeaturesModel>(cancellationToken: cancellationToken);
    }

    Task<GetTracksAudioAnalysisResponse> ITracksClient.GetTracksAudioAnalysis(string id, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return "https://api.spotify.com/v1/audio-analysis"
            .WithOAuthBearerToken(accessToken)
            .AppendPathSegment(id)
            .GetJsonAsync<GetTracksAudioAnalysisResponse>(cancellationToken: cancellationToken);
    }

    Task<GetRecommendationsResponse> ITracksClient.GetRecommendations(GetRecommendationsRequest request, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        var query = "https://api.spotify.com/v1/recommendations"
            .WithOAuthBearerToken(accessToken);

        if (request.SeedArtists != null)
        {
            query = query.SetQueryParam("seed_artists", request.SeedArtists);
        }
        
        if (request.SeedGenres != null)
        {
            query = query.SetQueryParam("seed_genres", request.SeedGenres);
        }
        
        if (request.SeedTracks != null)
        {
            query = query.SetQueryParam("seed_tracks", request.SeedTracks);
        }
        
        // TODO: other query params
        
        return query.GetJsonAsync<GetRecommendationsResponse>(cancellationToken);
    }
}