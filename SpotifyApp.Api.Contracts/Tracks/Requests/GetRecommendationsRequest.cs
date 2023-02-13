using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Tracks.Requests;

public sealed class GetRecommendationsRequest
{
    /// <summary>
    /// A comma separated list of Spotify IDs for seed artists.
    /// Up to 5 seed values may be provided in any combination of seed_artists, seed_tracks and seed_genres.
    /// </summary>
    [JsonProperty("seed_artists")]
    public required string SeedArtists { get; set; }

    [JsonProperty("seed_genres")]
    public required string SeedGenres { get; set; }

    [JsonProperty("seed_genres")]
    public required string SeedTracks { get; set; }

    [JsonProperty("limit")]
    public int Limit { get; set; }

    [JsonProperty("market")]
    public string Market { get; set; }

    [JsonProperty("max_acousticness")]
    public double MaxAcousticness { get; set; }

    [JsonProperty("max_danceability")]
    public double MaxDanceability { get; set; }

    [JsonProperty("max_duration_ms")]
    public int MaxDurationMs { get; set; }

    [JsonProperty("max_energy")]
    public double MaxEnergy { get; set; }

    [JsonProperty("max_instrumentalness")]
    public double MaxInstrumentalness { get; set; }

    [JsonProperty("max_key")]
    public int MaxKey { get; set; }

    [JsonProperty("max_liveness")]
    public double MaxLiveness { get; set; }

    [JsonProperty("max_loudness")]
    public double MaxLoudness { get; set; }

    [JsonProperty("max_mode")]
    public int MaxMode { get; set; }

    [JsonProperty("max_popularity")]
    public int MaxPopularity { get; set; }

    [JsonProperty("max_speechiness")]
    public double MaxSpeechiness { get; set; }

    [JsonProperty("max_tempo")]
    public double MaxTempo { get; set; }

    [JsonProperty("max_time_signature")]
    public int MaxTimeSignature { get; set; }

    [JsonProperty("max_valence")]
    public double MaxValence { get; set; }

    [JsonProperty("min_acousticness")]
    public double MinAcousticness { get; set; }

    [JsonProperty("min_danceability")]
    public double MinDanceability { get; set; }
    
    [JsonProperty("min_duration_ms")]
    public int MinDurationMs { get; set; } 
    
    [JsonProperty("min_energy")]
    public double MinEnergy { get; set; }

    [JsonProperty("min_instrumentalness")]
    public double MinInstrumentalness { get; set; }

    [JsonProperty("min_key")]
    public int MinKey { get; set; }

    [JsonProperty("min_liveness")]
    public double MinLiveness { get; set; }

    [JsonProperty("min_loudness")]
    public double MinLoudness { get; set; }

    [JsonProperty("min_mode")]
    public int MinMode { get; set; }

    [JsonProperty("min_popularity")]
    public int MinPopularity { get; set; }

    [JsonProperty("min_speechiness")]
    public double MinSpeechiness { get; set; }

    [JsonProperty("min_tempo")]
    public double MinTempo { get; set; }

    [JsonProperty("min_time_signature")]
    public int MinTimeSignature { get; set; }

    [JsonProperty("min_valence")]
    public double MinValence { get; set; }

    [JsonProperty("target_acousticness")]
    public double TargetAcousticness { get; set; }

    [JsonProperty("target_danceability")]
    public double TargetDanceability { get; set; }
    
    [JsonProperty("target_duration_ms")]
    public int TargetDurationMs { get; set; } 
    
    [JsonProperty("target_energy")]
    public double TargetEnergy { get; set; }

    [JsonProperty("target_instrumentalness")]
    public double TargetInstrumentalness { get; set; }

    [JsonProperty("target_key")]
    public int TargetKey { get; set; }

    [JsonProperty("target_liveness")]
    public double TargetLiveness { get; set; }

    [JsonProperty("target_loudness")]
    public double TargetLoudness { get; set; }

    [JsonProperty("target_mode")]
    public int TargetMode { get; set; }

    [JsonProperty("target_popularity")]
    public int TargetPopularity { get; set; }

    [JsonProperty("target_speechiness")]
    public double TargetSpeechiness { get; set; }

    [JsonProperty("target_tempo")]
    public double TargetTempo { get; set; }

    [JsonProperty("target_time_signature")]
    public int TargetTimeSignature { get; set; }

    [JsonProperty("target_valence")]
    public double TargetValence { get; set; }
}