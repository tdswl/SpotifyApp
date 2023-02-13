using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Tracks.Models.AudioAnalysis;

namespace SpotifyApp.Api.Contracts.Tracks.Responses;

public sealed class GetTracksAudioAnalysisResponse
{
    [JsonProperty("meta")]
    public MetaModel Meta { get; set; }
    
    [JsonProperty("track")]
    public SectionModel Track { get; set; }
    
    [JsonProperty("bars")]
    public IReadOnlyList<IntervalModel> Bars { get; set; }
    
    [JsonProperty("beats")]
    public IReadOnlyList<IntervalModel> Beats { get; set; }
    
    [JsonProperty("sections")]
    public IReadOnlyList<SectionModel> Sections { get; set; }
    
    [JsonProperty("segments")]
    public IReadOnlyList<SegmentModel> Segments { get; set; }
    
    [JsonProperty("tatums")]
    public IReadOnlyList<IntervalModel> Tatums { get; set; }
}