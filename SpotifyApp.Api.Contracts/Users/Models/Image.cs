using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Users.Models;

public sealed class Image
{
    [JsonProperty("url")]
    public required string Url { get; set; }
    
    [JsonProperty("height")]
    public int? Height { get; set; }
    
    [JsonProperty("width")]
    public int? Width { get; set; }
}