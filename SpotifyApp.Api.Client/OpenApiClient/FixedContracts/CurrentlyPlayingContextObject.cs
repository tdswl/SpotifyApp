namespace SpotifyApp.Api.Client.OpenApiClient;

public partial class CurrentlyPlayingContextObject
{
    /// <summary>
    /// Unix Millisecond Timestamp when data was fetched.
    /// </summary>
    /// <remarks>
    /// 'int' in generated model
    /// </remarks>

    [System.Text.Json.Serialization.JsonPropertyName("timestamp")]

    [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]   
    public long Timestamp { get; set; }
}