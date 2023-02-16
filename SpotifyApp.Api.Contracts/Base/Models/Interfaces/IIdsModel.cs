using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Base.Models.Interfaces;

public interface IIdsModel
{
    /// <summary>
    /// A comma-separated list of the Spotify IDs.
    /// For example: ids=4iV5W9uYEdYUVa79Axb7Rh,1301WleyT98MSxVHPZCA6M.
    /// Maximum: 50 IDs.
    /// Example value:"7ouMYWpwJ422jRcDASZB7P,4VqPOruhp5EdPBeR92t6lQ,2takcwOaAZWiXQijPHIx7B"
    /// </summary>
    [JsonProperty("ids")]
    string Ids { get; set; }
}