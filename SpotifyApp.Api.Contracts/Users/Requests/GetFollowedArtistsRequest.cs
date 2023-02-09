using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Users.Enums;

namespace SpotifyApp.Api.Contracts.Users.Requests;

public sealed class GetFollowedArtistsRequest
{
    /// <summary>
    /// The ID type: currently only artist is supported.
    /// Allowed value:"artist"
    /// Example value:"artist"
    /// </summary>
    [JsonProperty("type")]
    public required ItemsTypeApi Type { get; set; }
    
    /// <summary>
    /// The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.
    /// 0 - 50
    /// Default value:20
    /// Example value:10
    /// </summary>
    [JsonProperty("limit")]
    public int? Limit { get; set; }
    
    /// <summary>
    /// The last artist ID retrieved from the previous request.
    /// Example value:"0I2XqVXqHScXjHhk6AYYRe"
    /// </summary>
    [JsonProperty("after")]
    public string? After { get; set; }
}