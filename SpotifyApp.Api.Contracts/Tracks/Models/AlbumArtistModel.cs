using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Base.Enums;
using SpotifyApp.Api.Contracts.Users.Models;

namespace SpotifyApp.Api.Contracts.Tracks.Models;

public sealed class AlbumArtistModel
{
    /// <inheritdoc cref="Users.Models.ExternalUrls"/>
    [JsonProperty("external_urls")]
    public ExternalUrls? ExternalUrls { get; set; }
    
    /// <summary>
    /// A link to the Web API endpoint providing full details of the artist.
    /// </summary>
    [JsonProperty("href")]
    public required string Href { get; set; }
    
    /// <summary>
    /// The Spotify ID for the artist.
    /// </summary>
    [JsonProperty("id")]
    public required string Id { get; set; }
    
    /// <summary>
    /// The name of the artist.
    /// </summary>
    [JsonProperty("name")]
    public required string Name { get; set; }
    
    /// <summary>
    /// The object type.
    /// Allowed value:"artist"
    /// </summary>
    [JsonProperty("type")]
    public ItemsTypeApi Type { get; set; }
    
    /// <summary>
    /// The Spotify URI for the artist.
    /// </summary>
    [JsonProperty("uri")]
    public required string Uri { get; set; }
}