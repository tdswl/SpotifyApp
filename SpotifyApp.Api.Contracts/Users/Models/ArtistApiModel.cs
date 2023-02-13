using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Base.Models;

namespace SpotifyApp.Api.Contracts.Users.Models;

public sealed class ArtistApiModel
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
    public string? Next { get; set; }
    
    /// <inheritdoc cref="CursorModel"/>
    [JsonProperty("cursors")]
    public required CursorModel Cursors { get; set; }
    
    /// <summary>
    /// The total number of items available to return.
    /// </summary>
    [JsonProperty("total")]
    public required int Total { get; set; }
    
    [JsonProperty("items")]
    public required IReadOnlyCollection<ItemModelApi>? Items { get; set; }
}