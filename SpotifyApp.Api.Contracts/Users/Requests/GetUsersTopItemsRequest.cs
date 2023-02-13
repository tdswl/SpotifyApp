using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Base.Enums;
using SpotifyApp.Api.Contracts.Base.Requests;

namespace SpotifyApp.Api.Contracts.Users.Requests;

public sealed class GetUsersTopItemsRequest : PagedRequest
{
    /// <summary>
    /// The type of entity to return. Valid values: artists or tracks
    /// Allowed values:"artists""tracks"
    /// </summary>
    [JsonProperty("type")]
    public required ItemsTypeApi Type { get; set; }
    
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