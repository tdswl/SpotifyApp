using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Base.Models.Interfaces;
using SpotifyApp.Api.Contracts.Base.Requests;

namespace SpotifyApp.Api.Contracts.Albums.Requests;

public sealed class GetAlbumTracksRequest : PagedRequest, IIdModel
{
    /// <inheritdoc cref="IIdModel.Id"/>
    [JsonProperty("id")]
    public required string Id { get; set; }
    
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