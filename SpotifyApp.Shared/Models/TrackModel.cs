namespace SpotifyApp.Shared.Models;

public sealed class TrackModel : IItemWithImages
{
    public required string Id { get; set; }
    
    public required int Index { get; set; }
    
    public required string Name { get; set; }
    
    public required string ArtistName { get; set; }
    
    public required string AlbumName { get; set; }
    
    public bool Explicit { get; set; }   
    
    public required TimeSpan DurationMs { get; set; }

    public required IReadOnlyCollection<Image> Images { get; set; }
}