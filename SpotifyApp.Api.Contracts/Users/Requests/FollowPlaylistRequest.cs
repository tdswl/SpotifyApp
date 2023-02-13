using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Users.Requests;

public sealed class FollowPlaylistRequest
{
    /// <summary>
    /// Defaults to true. If true the playlist will be included in user's public playlists,
    /// if false it will remain private.
    /// </summary>
    [JsonProperty("public")]
    public required bool IsPublic { get; set; }
}