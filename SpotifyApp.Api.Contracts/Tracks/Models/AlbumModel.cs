using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Tracks.Enums;
using SpotifyApp.Api.Contracts.Users.Enums;
using SpotifyApp.Api.Contracts.Users.Models;

namespace SpotifyApp.Api.Contracts.Tracks.Models;

/// <summary>
/// The album on which the track appears. The album object
/// includes a link in href to full information about the album.
/// </summary>
public sealed class AlbumModel
{
    /// <summary>
    /// The type of the album.
    /// </summary>
    [JsonProperty("album_type")]
    public required AlbumTypeApi AlbumType { get; set; }
    
    /// <summary>
    /// The number of tracks in the album.
    /// </summary>
    [JsonProperty("total_tracks")]
    public required int TotalTracks { get; set; }
    
    /// <summary>
    /// The markets in which the album is available: ISO 3166-1 alpha-2 country codes.
    /// NOTE: an album is considered available in a market when at least 1 of
    /// its tracks is available in that market.
    /// </summary>
    [JsonProperty("available_markets")]
    public required IReadOnlyCollection<string>? AvailableMarkets { get; set; }
    
    /// <summary>
    /// Known external URLs for this album.
    /// </summary>
    [JsonProperty("external_urls")]
    public required ExternalUrls ExternalUrls { get; set; }
    
    /// <summary>
    /// A link to the Web API endpoint providing full details of the album.
    /// </summary>
    [JsonProperty("href")]
    public required string Href { get; set; }
    
    /// <summary>
    /// The Spotify ID for the album.
    /// </summary>
    [JsonProperty("id")]
    public required string Id { get; set; }
    
    /// <summary>
    /// The cover art for the album in various sizes, widest first.
    /// </summary>
    [JsonProperty("images")]
    public IReadOnlyCollection<Image>? Images { get; set; }
    
    /// <summary>
    /// The name of the album. In case of an album takedown, the value may be an empty string.
    /// </summary>
    [JsonProperty("name")]
    public required string Name { get; set; }
    
    /// <summary>
    /// The date the album was first released.
    /// </summary>
    [JsonProperty("release_date")]
    public required string ReleaseDate { get; set; }
    
    /// <summary>
    /// The precision with which release_date value is known.
    /// </summary>
    [JsonProperty("release_date_precision")]
    public required ReleaseDatePrecisionApi ReleaseDatePrecision { get; set; }
    
    /// <inheritdoc cref="RestrictionsModel"/>
    [JsonProperty("restrictions")]
    public RestrictionsModel? Restrictions { get; set; }
    
    /// <summary>
    /// The object type.
    /// Allowed value:"album"
    /// </summary>
    [JsonProperty("type")]
    public ItemsTypeApi Type { get; set; }
    
    /// <summary>
    /// The Spotify URI for the artist.
    /// </summary>
    [JsonProperty("uri")]
    public required string Uri { get; set; }   
    
    /// <summary>
    /// The copyright statements of the album.
    /// </summary>
    [JsonProperty("copyrights")]
    public IReadOnlyCollection<CopyrightModel>? Copyrights { get; set; }   
    
    /// <summary>
    /// Known external IDs for the album.
    /// </summary>
    [JsonProperty("external_ids")]
    public ExternalIdsModel? ExternalIds { get; set; }   
    
    /// <summary>
    /// A list of the genres the album is associated with. If not yet classified, the array is empty.
    /// </summary>
    [JsonProperty("genres")]
    public IReadOnlyCollection<string>? Genres { get; set; }    
    
    /// <summary>
    /// The label associated with the album.
    /// </summary>
    [JsonProperty("label")]
    public string? Label { get; set; }
    
    /// <summary>
    /// The popularity of the album. The value will be between 0 and 100, with 100 being the most popular.
    /// </summary>
    [JsonProperty("popularity")]
    public int? Popularity { get; set; }
    
    /// <summary>
    /// The field is present when getting an artist's albums. Compare to album_type this field represents relationship between the artist and the album.
    /// Allowed values:"album""single""compilation""appears_on"
    /// </summary>
    [JsonProperty("album_group")]
    public AlbumGroupApi? AlbumGroup { get; set; }
    
    /// <summary>
    /// The artists of the album.
    /// Each artist object includes a link in href to more detailed information about the artist.
    /// </summary>
    [JsonProperty("artists")]
    public IReadOnlyCollection<AlbumArtistModel>? Artists { get; set; }
}