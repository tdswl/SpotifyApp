using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Base.Models.Interfaces;
using SpotifyApp.Api.Contracts.Base.Requests;

namespace SpotifyApp.Api.Contracts.Albums.Requests;

public sealed class GetAlbumTracksRequest : PagedRequest, IIdModel, IMarketModel
{
    /// <inheritdoc cref="IIdModel.Id"/>
    [JsonProperty("id")]
    public required string Id { get; set; }
    
    /// <inheritdoc cref="IMarketModel.Market"/>
    [JsonProperty("market")]
    public string? Market { get; set; }
}