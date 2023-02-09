using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Users.Models;

/// <summary>
/// The cursors used to find the next set of items.
/// </summary>
public sealed class CursorModel
{
    /// <summary>
    /// The cursor to use as key to find the next page of items.
    /// </summary>
    [JsonProperty("after")]
    public string? After { get; set; }
    
    /// <summary>
    /// The cursor to use as key to find the previous page of items.
    /// </summary>
    [JsonProperty("before")]
    public string? Before { get; set; }
}