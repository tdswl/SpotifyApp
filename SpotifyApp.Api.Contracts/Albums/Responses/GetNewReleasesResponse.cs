using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Albums.Models;

namespace SpotifyApp.Api.Contracts.Albums.Responses;

public sealed class GetNewReleasesResponse
{
    [JsonProperty("albums")]
    public IReadOnlyCollection<AlbumApiModel>? Albums { get; set; }
}