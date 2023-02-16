using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Tracks.Models;

namespace SpotifyApp.Api.Contracts.Tracks.Responses;

public sealed class GetRecommendationsResponse
{
    [JsonProperty("seeds")]
    public IReadOnlyList<SeedsModel> Seeds { get; set; }
    
    [JsonProperty("tracks")]
    public IReadOnlyList<TrackApiModel> Tracks { get; set; }
}