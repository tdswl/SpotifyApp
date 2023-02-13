using Newtonsoft.Json;
using SpotifyApp.Api.Contracts.Base.Enums;

namespace SpotifyApp.Api.Contracts.Tracks.Models;

public sealed class SeedsModel
{
    /// <summary>
    /// The number of tracks available after min_* and max_* filters have been applied.
    /// </summary>
    [JsonProperty("afterFilteringSize")]
    public required int AfterFilteringSize { get; set; }
    
    /// <summary>
    /// The number of tracks available after relinking for regional availability.
    /// </summary>
    [JsonProperty("afterRelinkingSize")]
    public required int AfterRelinkingSize { get; set; }
    
    /// <summary>
    /// A link to the full track or artist data for this seed. For tracks this will be a link to a Track Object.
    /// For artists a link to an Artist Object. For genre seeds, this value will be null.
    /// </summary>
    [JsonProperty("href")]
    public required string Href { get; set; }
    
    /// <summary>
    /// The id used to select this seed. This will be the same as the
    /// string used in the seed_artists, seed_tracks or seed_genres parameter.
    /// </summary>
    [JsonProperty("id")]
    public required string Id { get; set; }
    
    /// <summary>
    /// The entity type of this seed. One of artist, track or genre.
    /// </summary>
    [JsonProperty("type")]
    public required ItemsTypeApi Type { get; set; }
    
    /// <summary>
    /// The number of recommended tracks available for this seed.
    /// </summary>
    [JsonProperty("initialPoolSize")]
    public required int InitialPoolSize { get; set; }
}