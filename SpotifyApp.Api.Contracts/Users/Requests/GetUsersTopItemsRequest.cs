using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Users.Enums;

namespace SpotifyApp.Api.Contracts.Users.Requests;

public sealed class GetUsersTopItemsRequest
{
    /// <summary>
    /// The type of entity to return. Valid values: artists or tracks
    /// Allowed values:"artists""tracks"
    /// </summary>
    [JsonProperty("type")]
    public required ItemsType Type { get; set; }
    
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

    /// <summary>
    /// Over what time frame the affinities are computed. Valid values: long_term
    /// (calculated from several years of data and including all
    /// new data as it becomes available),
    /// medium_term (approximately last 6 months),
    /// short_term (approximately last 4 weeks). Default: medium_term
    /// Default value:"medium_term"
    /// Example value:"medium_term"
    /// </summary>
    [JsonProperty("time_range")]
    public string? TimeRange { get; set; }
}