using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Tracks.Enums;

namespace SpotifyApp.Api.Contracts.Tracks.Models;

/// <summary>
/// Included in the response when a content restriction is applied.
/// </summary>
public sealed class RestrictionsModel
{
    /// <inheritdoc cref="RestrictionsReasonApi"/>
    [JsonProperty("reason")]
    public required RestrictionsReasonApi Reason { get; set; }
}