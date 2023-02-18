namespace SpotifyApp.Api.Client.OpenApiClient;

public partial class CursorObject
{ 
    /// <summary>
    /// The cursor to use as key to find the next page of items.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("after", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string? After { get; set; }
}