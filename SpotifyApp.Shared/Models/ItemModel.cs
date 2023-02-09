using SpotifyApp.Shared.Enums;

namespace SpotifyApp.Shared.Models;

public sealed class ItemModel : IItemWithImages
{
    public required string Id { get; set; }
    
    public required string Name { get; set; }

    public required IReadOnlyCollection<Image> Images { get; set; }
    
    public ItemType ItemType { get; set; }
}