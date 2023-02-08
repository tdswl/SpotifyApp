using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Base.Models;

namespace SpotifyApp.Api.Contracts.Base.Responses;

public sealed class ErrorResponse
{
    [JsonProperty("error")]
    public required ErrorModel Error { get; set; }
}