using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Users.Models;

/// <summary>
/// Information about the followers of the user.
/// </summary>
public sealed class Followers
{
    /// <summary>
    /// This will always be set to null, as the Web API does not support it at the moment.
    /// </summary>
    [JsonProperty("href")]
    public string? Href { get; set; }
    
    /// <summary>
    /// The total number of followers.
    /// </summary>
    [JsonProperty("total")]
    public int Total { get; set; }
}