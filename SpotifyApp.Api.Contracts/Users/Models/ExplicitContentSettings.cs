using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Users.Models;

public sealed class ExplicitContentSettings
{
    [JsonProperty("filter_enabled")]
    public bool FilterEnabled { get; set; }

    [JsonProperty("filter_locked")]
    public bool FilterLocked { get; set; }
}