using SpotifyApp.Shared.Enums;

namespace SpotifyApp.Shared.Models;

public sealed class AlbumModel : ISpotifyItem
{
    public required string Id { get; set; }
    
    public required string Name { get; set; }

    public required IReadOnlyCollection<ImageModel> Images { get; set; }

    public ItemType Type => ItemType.Album;
}