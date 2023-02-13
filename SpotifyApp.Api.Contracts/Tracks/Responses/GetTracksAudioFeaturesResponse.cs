using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Tracks.Models;

namespace SpotifyApp.Api.Contracts.Tracks.Responses;

public sealed class GetTracksAudioFeaturesResponse
{
    [JsonProperty("audio_features")]
    public required IReadOnlyList<AudioFeaturesModel> AudioFeatures { get; set; }
}