using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Albums.Models;
using SpotifyApp.Api.Contracts.Base.Responses;
using SpotifyApp.Api.Contracts.Tracks.Models;

namespace SpotifyApp.Api.Contracts.Albums.Responses;

public sealed class GetAlbumResponse : AlbumApiModel
{
    [JsonProperty("tracks")]
    public IReadOnlyCollection<PagedResponse<TrackApiModel>>? Tracks { get; set; }
}