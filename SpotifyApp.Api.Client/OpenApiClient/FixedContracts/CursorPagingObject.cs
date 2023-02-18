namespace SpotifyApp.Api.Client.OpenApiClient;

public partial class CursorPagingObject
{
    /// <summary>
    /// URL to the next page of items. ( `null` if none)
    /// </summary>
    [Newtonsoft.Json.JsonProperty("next", Required = Newtonsoft.Json.Required.AllowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string? Next { get; set; }
}