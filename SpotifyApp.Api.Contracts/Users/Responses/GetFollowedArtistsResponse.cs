using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Users.Models;

namespace SpotifyApp.Api.Contracts.Users.Responses;

public sealed class GetFollowedArtistsResponse
{
    /// <summary>
    ///     A paged set of artists
    /// </summary>
    [JsonProperty("artists")]
    public required ArtistModel Artists { get; set; }
}