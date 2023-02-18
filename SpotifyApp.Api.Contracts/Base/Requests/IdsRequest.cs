using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Base.Models.Interfaces;

namespace SpotifyApp.Api.Contracts.Base.Requests;

public sealed class IdsRequest : IIdsModel, IMarketModel
{
    /// <inheritdoc cref="IIdsModel.Ids"/>
    [JsonProperty("ids")]
    public required string Ids { get; set; }
    
    /// <inheritdoc cref="IMarketModel.Market"/>
    [JsonProperty("market")]
    public string? Market { get; set; }
}