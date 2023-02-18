using Flurl.Http;
using SpotifyApp.Api.Client.Extensions;
using SpotifyApp.Api.Contracts.Base.Requests;
using SpotifyApp.Api.Contracts.Tracks.Models;
using SpotifyApp.Api.Contracts.Tracks.Requests;
using SpotifyApp.Api.Contracts.Tracks.Responses;

namespace SpotifyApp.Api.Client.Tracks;

internal class TracksClient : ITracksClient
{
    Task<TrackApiModel> ITracksClient.GetTrack(IdRequest request, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return TracksRoutes.GetTrack
            .WithOAuthBearerToken(accessToken)
            .AppendPathSegment(request.Id)
            .SetMarketParams(request)
            .GetJsonAsync<TrackApiModel>(cancellationToken);
    }

    Task<GetSeveralTracksResponse> ITracksClient.GetSeveralTracks(IdsRequest request, 
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return TracksRoutes.GetTrack
            .WithOAuthBearerToken(accessToken)
            .SetQueryParam("ids", request.Ids)
            .SetMarketParams(request)
            .GetJsonAsync<GetSeveralTracksResponse>(cancellationToken);
    }

    Task<GetUsersSavedTracksResponse> ITracksClient.GetUsersSavedTracks(GetUsersSavedTracksRequest request,
        string accessToken, 
        CancellationToken cancellationToken)
    {
        return TracksRoutes.GetUsersSavedTracks
            .WithOAuthBearerToken(accessToken)
            .SetPagedQueryParams(request)
            .SetMarketParams(request)
            .GetJsonAsync<GetUsersSavedTracksResponse>(cancellationToken);
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