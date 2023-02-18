using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Base.Models.Interfaces;
using SpotifyApp.Api.Contracts.Base.Requests;

namespace SpotifyApp.Api.Contracts.Tracks.Requests;

public sealed class GetUsersSavedTracksRequest : PagedRequest, IMarketModel
{
    /// <inheritdoc cref="IMarketModel.Market"/>
    [JsonProperty("market")]
    public string? Market { get; set; }
}