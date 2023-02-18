using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Base.Models.Interfaces;
using SpotifyApp.Api.Contracts.Base.Requests;
using SpotifyApp.Api.Contracts.Tracks.Enums;

namespace SpotifyApp.Api.Contracts.Artists.Requests;

public sealed class GetArtistsAlbumsRequest : PagedRequest, IMarketModel
{
    /// <summary>
    /// A comma-separated list of keywords that will be used to filter the response. If not supplied, all album types will be returned.
    /// Valid values are:
    /// - album
    /// - single
    /// - appears_on
    /// - compilation
    ///     For example: include_groups=album,single.
    ///     Example value:"single,appears_on"
    /// </summary>
    [JsonProperty("include_groups")]
    public IReadOnlyList<AlbumGroupApi>? IncludeGroups { get; set; }
    
    /// <inheritdoc cref="IMarketModel.Market"/>
    [JsonProperty("market")]
    public string? Market { get; set; }
}