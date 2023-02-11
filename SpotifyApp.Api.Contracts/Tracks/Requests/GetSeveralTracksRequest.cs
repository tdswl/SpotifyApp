using Newtonsoft.Json;

namespace SpotifyApp.Api.Contracts.Tracks.Requests;

public sealed class GetSeveralTracksRequest
{
    /// <summary>
    /// A comma-separated list of the Spotify IDs. For example: ids=4iV5W9uYEdYUVa79Axb7Rh,1301WleyT98MSxVHPZCA6M. Maximum: 50 IDs.
    /// Example value:"7ouMYWpwJ422jRcDASZB7P,4VqPOruhp5EdPBeR92t6lQ,2takcwOaAZWiXQijPHIx7B"
    /// </summary>
    [JsonProperty("ids")]
    public required string Ids { get; set; }
    
    /// <summary>
    /// An ISO 3166-1 alpha-2 country code. If a country code is specified, only content that is available in that market will be returned.
    /// If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter.
    ///    Note: If neither market or user country are provided, the content is considered unavailable for the client.
    ///    Users can view the country that is associated with their account in the account settings.
    ///    Example value:"ES"
    /// </summary>
    [JsonProperty("market")]
    public string? Market { get; set; }
}