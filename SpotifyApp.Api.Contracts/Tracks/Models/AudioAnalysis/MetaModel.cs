using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Tracks.Models.AudioAnalysis;

public sealed class MetaModel
{
    /// <summary>
    /// The version of the Analyzer used to analyze this track.
    /// </summary>
    [JsonProperty("analyzer_version")]
    public string AnalyzerVersion { get; set; }
    
    /// <summary>
    /// The platform used to read the track's audio data.
    /// </summary>
    [JsonProperty("platform")]
    public string Platform { get; set; }
    
    /// <summary>
    /// A detailed status code for this track. If analysis data is missing, this code may explain why.
    /// </summary>
    [JsonProperty("detailed_status")]
    public string DetailedStatus { get; set; }
    
    /// <summary>
    /// The return code of the analyzer process. 0 if successful, 1 if any errors occurred.
    /// </summary>
    [JsonProperty("status_code")]
    public int StatusCode { get; set; }
    
    /// <summary>
    /// The Unix timestamp (in seconds) at which this track was analyzed.
    /// </summary>
    [JsonProperty("timestamp")]
    public int Timestamp { get; set; }
    
    /// <summary>
    /// The amount of time taken to analyze this track.
    /// </summary>
    [JsonProperty("analysis_time")]
    public double AnalysisTime { get; set; }
    
    /// <summary>
    /// The method used to read the track's audio data.
    /// </summary>
    [JsonProperty("input_process")]
    public string InputProcess { get; set; }
}