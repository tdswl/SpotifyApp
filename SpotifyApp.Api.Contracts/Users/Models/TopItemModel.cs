using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Users.Enums;

namespace SpotifyApp.Api.Contracts.Users.Models;

public sealed class TopItemModel
{
    /// <inheritdoc cref="Models.ExternalUrls"/>
    [JsonProperty("external_urls")]
    public ExternalUrls? ExternalUrls { get; set; }
    
    /// <inheritdoc cref="Models.Followers"/>
    [JsonProperty("followers")]
    public Followers? Followers { get; set; }
    
    /// <summary>
    /// A list of the genres the artist is associated with. If not yet classified, the array is empty.
    /// </summary>
    [JsonProperty("genres")]
    public List<string>? Genres { get; set; }
    
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
    /// Images of the artist in various sizes, widest first.
    /// </summary>
    [JsonProperty("images")]
    public List<Image>? Images { get; set; }
    
    /// <summary>
    /// The name of the artist.
    /// </summary>
    [JsonProperty("name")]
    public required string Name { get; set; }
    
    /// <summary>
    /// The popularity of the artist. The value will be between 0 and 100, with 100 being the most popular.
    /// The artist's popularity is calculated from the popularity of all the artist's tracks.
    /// </summary>
    [JsonProperty("popularity")]
    public int Popularity { get; set; }
    
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