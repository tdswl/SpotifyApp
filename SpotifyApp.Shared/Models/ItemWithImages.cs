using SpotifyApp.Shared.Enums;

namespace SpotifyApp.Shared.Models;

public sealed class ItemWithImages
{
    public string Id { get; set; }
    
    public string Name { get; set; }

    public IReadOnlyCollection<Image> Images { get; set; }
    
    public ItemType ItemType { get; set; }
}