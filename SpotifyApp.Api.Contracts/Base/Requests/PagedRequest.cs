using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Base.Requests;

public abstract class PagedRequest
{
    /// <summary>
    /// The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.
    /// 0 - 50
    /// Default value:20
    /// Example value:10
    /// </summary>
    [JsonProperty("limit")]
    public int? Limit { get; set; }
    
    /// <summary>
    /// The index of the first item to return. Default: 0 (the first item).
    /// Use with limit to get the next set of items.
    /// Default value:0
    /// Example value:5
    /// </summary>
    [JsonProperty("offset")]
    public int? Offset { get; set; }
}