using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Base.Models;
using SpotifyApp.Api.Contracts.Tracks.Models;
using SpotifyApp.Api.Contracts.Users.Enums;
using SpotifyApp.Api.Contracts.Users.Models;

namespace SpotifyApp.Api.Contracts.Tracks.Responses;

public sealed class GetTrackResponse
{
    /// <summary>
    /// The album on which the track appears.
    /// The album object includes a link in href to full information about the album.
    /// </summary>
    [JsonProperty("album")]
    public required AlbumApiModel Album { get; set; }
    
    /// <summary>
    /// The artists who performed the track.
    /// Each artist object includes a link in href to more detailed information about the artist.
    /// </summary>
    [JsonProperty("artist")]
    public required ItemModelApi Artist { get; set; }
    
    /// <summary>
    /// A list of the countries in which the track can be played, identified by their ISO 3166-1 alpha-2 code.
    /// </summary>
    [JsonProperty("available_markets")]
    public IReadOnlyCollection<string>? AvailableMarkets { get; set; }
    
    /// <summary>
    /// The disc number (usually 1 unless the album consists of more than one disc).
    /// </summary>
    [JsonProperty("disc_number")]
    public int? DiscNumber { get; set; }   
    
    /// <summary>
    /// The track length in milliseconds.
    /// </summary>
    [JsonProperty("duration_ms")]
    public required int DurationMs { get; set; }   
    
    /// <summary>
    /// Whether or not the track has explicit lyrics ( true = yes it does; false = no it does not OR unknown).
    /// </summary>
    [JsonProperty("explicit")]
    public bool Explicit { get; set; }   
    
    /// <summary>
    /// Known external IDs for the track.
    /// </summary>
    [JsonProperty("external_ids")]
    public ExternalIdsModel? ExternalIds { get; set; }   
    
    /// <summary>
    /// Known external URLs for this track.
    /// </summary>
    [JsonProperty("external_urls")]
    public ExternalUrls? ExternalUrls { get; set; }
    
    /// <summary>
    /// A link to the Web API endpoint providing full details of the track.
    /// </summary>
    [JsonProperty("href")]
    public required string Href { get; set; }
    
    /// <summary>
    /// The Spotify ID for the track.
    /// </summary>
    [JsonProperty("id")]
    public required string Id { get; set; }
    
    /// <summary>
    /// Part of the response when Track Relinking is applied.
    /// If true, the track is playable in the given market. Otherwise false.
    /// </summary>
    [JsonProperty("is_playable")]
    public bool? IsPlayable { get; set; }   
    
    /// <summary>
    /// Whether or not the track has explicit lyrics ( true = yes it does; false = no it does not OR unknown).
    /// </summary>
    [JsonProperty("linked_from")]
    public object? LinkedFrom { get; set; }   
    
    /// <inheritdoc cref="RestrictionsModel"/>
    [JsonProperty("restrictions")]
    public RestrictionsModel? Restrictions { get; set; }
    
    /// <summary>
    /// The name of the track.
    /// </summary>
    [JsonProperty("name")]
    public required string Name { get; set; }
    
    /// <summary>
    /// The popularity of the track. The value will be between 0 and 100, with 100 being the most popular.
    /// The popularity of a track is a value between 0 and 100, with 100 being the most popular.
    /// The popularity is calculated by algorithm and is based, in the most part, on the total
    /// number of plays the track has had and how recent those plays are.
    /// Generally speaking, songs that are being played a lot now will have a higher
    /// popularity than songs that were played a lot in the past. Duplicate tracks
    /// (e.g. the same track from a single and an album) are rated independently.
    /// Artist and album popularity is derived mathematically from track popularity.
    /// Note: the popularity value may lag actual popularity by a few days: the value is not updated in real time.
    /// </summary>
    [JsonProperty("popularity")]
    public int? Popularity { get; set; }
    
    /// <summary>
    /// A link to a 30 second preview (MP3 format) of the track. Can be null
    /// </summary>
    [JsonProperty("preview_url")]
    public string? PreviewUrl { get; set; }
    
    /// <summary>
    /// The number of the track. If an album has several discs, the track number is the number on the specified disc.
    /// </summary>
    [JsonProperty("track_number")]
    public required int TrackNumber { get; set; }
    
    /// <summary>
    /// The object type.
    /// Allowed value:"track"
    /// </summary>
    [JsonProperty("type")]
    public ItemsTypeApi Type { get; set; }
    
    /// <summary>
    /// The Spotify URI for the track.
    /// </summary>
    [JsonProperty("uri")]
    public required string Uri { get; set; }   
    
    /// <summary>
    /// Whether or not the track is from a local file.
    /// </summary>
    [JsonProperty("is_local")]
    public bool IsLocal { get; set; }
}