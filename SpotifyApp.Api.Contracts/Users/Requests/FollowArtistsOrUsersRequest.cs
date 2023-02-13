using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Users.Enums;

namespace SpotifyApp.Api.Contracts.Users.Requests;

public sealed class FollowArtistsOrUsersRequest
{
    /// <summary>
    /// A comma-separated list of the artist or the user Spotify IDs. A maximum of 50 IDs can be sent in one request.
    /// Example value:"2CIMQHirSU0MQqyYHq0eOx,57dN52uHvrHOxijzpIgu3E,1vCWHaC5f2uS3yhpwWbIA6"
    /// </summary>
    [JsonProperty("ids")]
    public required string Ids { get; set; }
    
    /// <summary>
    /// The ID type.
    /// Allowed values:"artist""user"
    /// Example value:"artist"
    /// </summary>
    [JsonProperty("type")]
    public required ItemsTypeApi Type { get; set; }
}