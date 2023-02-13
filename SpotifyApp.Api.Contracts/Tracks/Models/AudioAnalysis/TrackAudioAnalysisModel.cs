using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Tracks.Models.AudioAnalysis;

public sealed class TrackAudioAnalysisModel
{
    /// <summary>
    /// The exact number of audio samples analyzed from this track. See also analysis_sample_rate.
    /// </summary>
    [JsonProperty("num_samples")]
    public int NumSamples { get; set; }
    
    /// <summary>
    /// Length of the track in seconds.
    /// </summary>
    [JsonProperty("duration")]
    public double Duration { get; set; }
    
    /// <summary>
    /// This field will always contain the empty string.
    /// </summary>
    [JsonProperty("sample_md5")]
    public string SampleMd5 { get; set; }
    
    /// <summary>
    /// An offset to the start of the region of the track that was analyzed.
    /// (As the entire track is analyzed, this should always be 0.)
    /// </summary>
    [JsonProperty("offset_seconds")]
    public int OffsetSeconds { get; set; }
    
    /// <summary>
    /// An offset to the start of the region of the track that was analyzed.
    /// (As the entire track is analyzed, this should always be 0.)
    /// </summary>
    [JsonProperty("window_seconds")]
    public int WindowSeconds { get; set; }
    
    /// <summary>
    /// The sample rate used to decode and analyze this track. May differ
    /// from the actual sample rate of this track available on Spotify.
    /// </summary>
    [JsonProperty("analysis_sample_rate")]
    public int AnalysisSampleRate { get; set; }
    
    /// <summary>
    /// The number of channels used for analysis.
    /// If 1, all channels are summed together to mono before analysis.
    /// </summary>
    [JsonProperty("analysis_channels")]
    public int AnalysisChannels { get; set; }
    
    /// <summary>
    /// The time, in seconds, at which the track's fade-in period ends.
    /// If the track has no fade-in, this will be 0.0
    /// </summary>
    [JsonProperty("end_of_fade_in")]
    public int EndOfFadeIn { get; set; }
    
    /// <summary>
    /// The time, in seconds, at which the track's fade-out period starts.
    /// If the track has no fade-out, this should match the track's length.
    /// </summary>
    [JsonProperty("start_of_fade_out")]
    public double StartOfFadeOut { get; set; }
    
    /// <summary>
    /// TThe overall loudness of a track in decibels (dB). Loudness values are averaged across the entire
    /// track and are useful for comparing relative loudness of tracks.
    /// Loudness is the quality of a sound that is the primary psychological correlate of physical strength (amplitude). Values typically range between -60 and 0 db.
    /// </summary>
    [JsonProperty("loudness")]
    public double Loudness { get; set; }
    
    /// <summary>
    /// The overall estimated tempo of a track in beats per minute (BPM). In musical terminology, tempo is the speed or pace o
    /// a given piece and derives directly from the average beat duration.
    /// </summary>
    [JsonProperty("tempo")]
    public double Tempo { get; set; }
    
    /// <summary>
    /// The confidence, from 0.0 to 1.0, of the reliability of the tempo.
    /// </summary>
    [JsonProperty("tempo_confidence")]
    public double TempoConfidence { get; set; }
    
    /// <summary>
    /// An estimated time signature. The time signature (meter) is a notational convention to specify how many beats
    /// are in each bar (or measure). The time signature ranges from 3 to 7 indicating time signatures of "3/4", to "7/4".
    /// </summary>
    [JsonProperty("time_signature")]
    public int TimeSignature { get; set; }
    
    /// <summary>
    /// The confidence, from 0.0 to 1.0, of the reliability of the time_signature.
    /// </summary>
    [JsonProperty("time_signature_confidence")]
    public double TimeSignatureConfidence { get; set; }
    
    /// <summary>
    /// The key the track is in. Integers map to pitches using standard Pitch Class notation. E.g. 0 = C, 1 = C♯/D♭,
    /// 2 = D, and so on. If no key was detected, the value is -1.
    /// </summary>
    [JsonProperty("key")]
    public int Key { get; set; }
    
    /// <summary>
    /// The confidence, from 0.0 to 1.0, of the reliability of the key.
    /// </summary>
    [JsonProperty("key_confidence")]
    public double KeyConfidence { get; set; }
    
    /// <summary>
    /// Mode indicates the modality (major or minor) of a track, the type of scale from which its melodic
    /// content is derived. Major is represented by 1 and minor is 0.
    /// </summary>
    [JsonProperty("mode")]
    public int Mode { get; set; }
    
    /// <summary>
    /// The confidence, from 0.0 to 1.0, of the reliability of the mode.
    /// </summary>
    [JsonProperty("mode_confidence")]
    public double ModeConfidence { get; set; }
    
    /// <summary>
    /// An Echo Nest Musical Fingerprint (ENMFP) codestring for this track.
    /// </summary>
    [JsonProperty("codestring")]
    public string Codestring { get; set; }
    
    /// <summary>
    /// A version number for the Echo Nest Musical Fingerprint format used in the codestring field.
    /// </summary>
    [JsonProperty("code_version")]
    public double CodeVersion { get; set; }
    
    /// <summary>
    /// An EchoPrint codestring for this track.
    /// </summary>
    [JsonProperty("echoprintstring")]
    public string Echoprintstring { get; set; }
    
    /// <summary>
    /// A version number for the EchoPrint format used in the echoprintstring field.
    /// </summary>
    [JsonProperty("echoprint_version")]
    public double EchoprintVersion { get; set; }
    
    /// <summary>
    /// A Synchstring for this track.
    /// </summary>
    [JsonProperty("synchstring")]
    public string Synchstring { get; set; }
    
    /// <summary>
    /// A version number for the Synchstring used in the synchstring field.
    /// </summary>
    [JsonProperty("synch_version")]
    public int SynchVersion { get; set; }
    
    /// <summary>
    /// A Rhythmstring for this track. The format of this string is similar to the Synchstring.
    /// </summary>
    [JsonProperty("rhythmstring")]
    public string Rhythmstring { get; set; }
    
    /// <summary>
    /// A version number for the Rhythmstring used in the rhythmstring field.
    /// </summary>
    [JsonProperty("rhythm_version")]
    public int RhythmVersion { get; set; }
}