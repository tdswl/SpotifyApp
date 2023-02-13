using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Base.Enums;
using SpotifyApp.Api.Contracts.Users.Models;

namespace SpotifyApp.Api.Contracts.Users.Responses;

public sealed class GetUsersProfileResponse
{
    /// <summary>
    ///     The name displayed on the user's profile. null if not available.
    /// </summary>
    [JsonProperty("display_name")]
    public string? DisplayName { get; set; }

    /// <inheritdoc cref="Models.ExternalUrls" />
    [JsonProperty("external_urls")]
    public ExternalUrls? ExternalUrls { get; set; }

    /// <inheritdoc cref="Models.Followers" />
    [JsonProperty("followers")]
    public Followers? Followers { get; set; }

    /// <summary>
    ///     A link to the Web API endpoint for this user.
    /// </summary>
    [JsonProperty("href")]
    public required string Href { get; set; }

    /// <summary>
    ///     The Spotify user ID for the user.
    /// </summary>
    [JsonProperty("id")]
    public required string Id { get; set; }

    /// <summary>
    ///     The user's profile image.
    /// </summary>
    [JsonProperty("images")]
    public IReadOnlyCollection<ImageModelApi>? Images { get; set; }

    /// <summary>
    ///     The object type: "user"
    /// </summary>
    [JsonProperty("type")]
    public required ItemsTypeApi Type { get; set; }

    /// <summary>
    ///     The Spotify URI for the user.
    /// </summary>
    [JsonProperty("uri")]
    public string? Uri { get; set; }
}