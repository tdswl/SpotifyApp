using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Users.Models;

/// <summary>
/// Image model
/// </summary>
public sealed class Image
{
    /// <summary>
    /// The source URL of the image.
    /// </summary>
    [JsonProperty("url")]
    public required string Url { get; set; }
    
    /// <summary>
    /// The image height in pixels.
    /// </summary>
    [JsonProperty("height")]
    public required int? Height { get; set; }
    
    /// <summary>
    /// The image width in pixels.
    /// </summary>
    [JsonProperty("width")]
    public required int? Width { get; set; }
}