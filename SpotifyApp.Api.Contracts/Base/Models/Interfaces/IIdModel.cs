using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Base.Models.Interfaces;

public interface IIdModel
{
    /// <summary>
    /// The Spotify ID for the object.
    /// Example value:"11dFghVXANMlKmJXsNCbNl"
    /// </summary>
    [JsonProperty("id")]
    string Id { get; set; }
}