using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Tracks.Models;

public sealed class ExternalIdsModel
{
    /// <summary>
    /// International Standard Recording Code
    /// </summary>
    [JsonProperty("isrc")]
    public required string Isrc { get; set; }

    /// <summary>
    /// International Article Number
    /// </summary>
    [JsonProperty("ean")]
    public required string Ean { get; set; }

    /// <summary>
    /// Universal Product Code
    /// </summary>
    [JsonProperty("upc")]
    public required string Upc { get; set; }   
}