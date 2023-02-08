using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Base.Models;

public sealed class ErrorModel
{
    /// <summary>
    /// The HTTP status code (also returned in the response header; see Response Status Codes for more information).
    /// 400 - 599
    /// </summary>
    [JsonProperty("status")]
    public required int Status { get; set; }

    /// <summary>
    /// A short description of the cause of the error.
    /// </summary>
    [JsonProperty("message")]
    public required string Message { get; set; }
}