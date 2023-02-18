using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Base.Models.Interfaces;
using SpotifyApp.Api.Contracts.Base.Requests;

namespace SpotifyApp.Api.Contracts.Albums.Requests;

public sealed class GetUsersSavedAlbumsRequest : PagedRequest, IMarketModel
{
    /// <inheritdoc cref="IMarketModel.Market"/>
    [JsonProperty("market")]
    public string? Market { get; set; }
}