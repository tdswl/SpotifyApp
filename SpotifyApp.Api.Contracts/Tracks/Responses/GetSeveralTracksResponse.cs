using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Tracks.Models;

namespace SpotifyApp.Api.Contracts.Tracks.Responses;

public sealed class GetSeveralTracksResponse
{
    /// <summary>
    /// A set of tracks
    /// </summary>
    [JsonProperty("tracks")]
    public required IReadOnlyList<TrackApiModel> Tracks { get; set; }
}