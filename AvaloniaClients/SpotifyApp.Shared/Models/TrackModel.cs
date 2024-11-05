using SpotifyApp.Shared.Enums;

namespace SpotifyApp.Shared.Models;

public sealed class TrackModel : ISpotifyItem
{
    public required string Id { get; set; }
    
    public required string Name { get; set; }

    public required IReadOnlyCollection<ImageModel> Images { get; set; }

    public ItemType Type => ItemType.Track;
    
    public required int Index { get; set; }
    public required string ArtistName { get; set; }
    
    public required string AlbumName { get; set; }
    
    public bool Explicit { get; set; }   
    
    /// <summary>
    /// string in mm:ss format
    /// </summary>
    public required string DurationMs { get; set; }
}