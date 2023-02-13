using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Tracks.Models.AudioAnalysis;

public class IntervalModel
{
    /// <summary>
    /// The starting point (in seconds) of the time interval.
    /// </summary>
    [JsonProperty("start")]
    public double Start { get; set; }
    
    /// <summary>
    /// The duration (in seconds) of the time interval.
    /// </summary>
    [JsonProperty("duration")]
    public double Duration { get; set; }
    
    /// <summary>
    /// The confidence, from 0.0 to 1.0, of the reliability of the interval.
    /// </summary>
    [JsonProperty("confidence")]
    public double Confidence { get; set; }
}