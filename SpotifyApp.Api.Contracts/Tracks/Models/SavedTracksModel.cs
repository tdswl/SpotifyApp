using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Tracks.Responses;

namespace SpotifyApp.Api.Contracts.Tracks.Models;

public sealed class SavedTracksModel
{
    /// <summary>
    /// The date and time the track was saved. Timestamps are returned in ISO 8601
    /// format as Coordinated Universal Time (UTC) with a zero offset: YYYY-MM-DDTHH:MM:SSZ.
    /// If the time is imprecise (for example, the date/time of an album release),
    /// an additional field indicates the precision; see for example, release_date in an album object.
    /// </summary>
    [JsonProperty("added_at")]
    public required DateTimeOffset AddedAt { get; set; }
    
    /// <summary>
    /// Information about the track.
    /// </summary>
    [JsonProperty("track")]
    public required GetTrackResponse Track { get; set; }
}