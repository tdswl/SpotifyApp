using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Base.Models.Interfaces;

namespace SpotifyApp.Api.Contracts.Base.Requests;

public sealed class IdRequest : IIdModel, IMarketModel
{
    /// <inheritdoc cref="IIdModel.Id"/>
    [JsonProperty("id")]
    public required string Id { get; set; }
    
    /// <inheritdoc cref="IMarketModel.Market"/>
    [JsonProperty("market")]
    public string? Market { get; set; }
}