using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Base.Enums;
using SpotifyApp.Api.Contracts.Base.Models.Interfaces;
using SpotifyApp.Api.Contracts.Base.Requests;

namespace SpotifyApp.Api.Contracts.Users.Requests;

public sealed class FollowArtistsOrUsersRequest : IIdsModel
{
    /// <inheritdoc cref="IIdsModel.Ids"/>
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