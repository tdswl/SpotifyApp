using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Tracks.Models;

/// <summary>
/// The copyright statements of the object
/// </summary>
public sealed class CopyrightModel
{
    /// <summary>
    /// The copyright text for this content.
    /// </summary>
    [JsonProperty("text")]
    public required string Text { get; set; }   
    
    /// <summary>
    /// The type of copyright: C = the copyright, P = the sound recording (performance) copyright.
    /// </summary>
    [JsonProperty("type")]
    public required string Type { get; set; }   
}