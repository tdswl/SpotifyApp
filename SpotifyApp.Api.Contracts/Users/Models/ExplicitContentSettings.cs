using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Users.Models;

/// <summary>
/// The user's explicit content settings.
/// This field is only available when the current user has
/// granted access to the user-read-private scope.
/// </summary>
public sealed class ExplicitContentSettings
{
    /// <summary>
    /// When true, indicates that explicit content should not be played.
    /// </summary>
    [JsonProperty("filter_enabled")]
    public bool FilterEnabled { get; set; }

    /// <summary>
    /// When true, indicates that the explicit content setting is locked and can't be changed by the user.
    /// </summary>
    [JsonProperty("filter_locked")]
    public bool FilterLocked { get; set; }
}