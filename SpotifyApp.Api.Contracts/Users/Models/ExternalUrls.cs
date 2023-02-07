using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Users.Models;

public sealed class ExternalUrls
{
    [JsonProperty("spotify")]
    public required string Spotify { get; set; }
}