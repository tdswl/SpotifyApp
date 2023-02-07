using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Users.Models;

public sealed class Followers
{
    [JsonProperty("href")]
    public required string Href { get; set; }
    
    [JsonProperty("total")]
    public int Total { get; set; }
}