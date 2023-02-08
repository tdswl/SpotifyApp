using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Users.Models;

namespace SpotifyApp.Api.Contracts.Users.Responses;

public sealed class GetUsersTopItemsResponse
{
    /// <summary>
    /// A link to the Web API endpoint returning the full result of the request
    /// </summary>
    [JsonProperty("href")]
    public required string Href { get; set; }
    
    /// <summary>
    /// The maximum number of items in the response (as set in the query or by default).
    /// </summary>
    [JsonProperty("limit")]
    public required int Limit { get; set; }
    
    /// <summary>
    /// URL to the next page of items. ( null if none)
    /// </summary>
    [JsonProperty("next")]
    public required string? Next { get; set; }
    
    /// <summary>
    /// The offset of the items returned (as set in the query or by default)
    /// </summary>
    [JsonProperty("offset")]
    public required int Offset { get; set; }
    
    /// <summary>
    /// URL to the previous page of items. ( null if none)
    /// </summary>
    [JsonProperty("previous")]
    public required string? Previous { get; set; }
    
    /// <summary>
    /// The total number of items available to return.
    /// </summary>
    [JsonProperty("total")]
    public required int Total { get; set; }
    
    [JsonProperty("items")]
    public required List<TopItemModel>? Items { get; set; }
}