using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Users.Models;

/// <summary>
///     Known external URLs for this user.
/// </summary>
public sealed class ExternalUrls
{
    /// <summary>
    ///     The Spotify URL for the object.
    /// </summary>
    [JsonProperty("spotify")]
    public required string Spotify { get; set; }
}