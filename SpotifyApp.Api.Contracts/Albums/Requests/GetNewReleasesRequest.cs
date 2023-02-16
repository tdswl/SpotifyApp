using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Base.Requests;

namespace SpotifyApp.Api.Contracts.Albums.Requests;

public sealed class GetNewReleasesRequest : PagedRequest
{
    /// <summary>
    /// A country: an ISO 3166-1 alpha-2 country code. Provide this parameter
    /// if you want the list of returned items to be relevant to a particular country.
    /// If omitted, the returned items will be relevant to all countries.
    /// </summary>
    [JsonProperty("country")]
    public string? Country { get; set; }
}