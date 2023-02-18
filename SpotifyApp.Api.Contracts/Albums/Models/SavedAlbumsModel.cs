using Newtonsoft.Json;
namespace SpotifyApp.Api.Contracts.Albums.Models;

public sealed class SavedAlbumsModel
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
    /// Information about the album.
    /// </summary>
    [JsonProperty("album")]
    public required AlbumApiModel Album { get; set; }
}