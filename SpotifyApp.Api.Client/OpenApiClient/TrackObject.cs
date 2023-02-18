namespace SpotifyApp.Api.Client.OpenApiClient;

public partial class TrackObject
{
    /// <summary>
    /// A link to a 30 second preview (MP3 format) of the track. Can be `null`
    /// <br/>
    /// </summary>
    [Newtonsoft.Json.JsonProperty("preview_url", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string? Preview_url { get; set; }
}