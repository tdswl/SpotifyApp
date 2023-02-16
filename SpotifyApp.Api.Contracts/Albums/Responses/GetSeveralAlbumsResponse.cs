using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Albums.Responses;

public sealed class GetSeveralAlbumsResponse
{
    [JsonProperty("albums")]
    public IReadOnlyCollection<GetAlbumResponse>? Albums { get; set; }
}