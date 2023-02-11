using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Tracks.Responses;

public sealed class GetSeveralTracksResponse
{
    /// <summary>
    /// A set of tracks
    /// </summary>
    [JsonProperty("tracks")]
    public required IReadOnlyList<GetTrackResponse> Tracks { get; set; }
}