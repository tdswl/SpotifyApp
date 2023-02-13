using SpotifyApp.Shared.Enums;

namespace SpotifyApp.Shared.Models;

public interface ISpotifyItem
{
    string Id { get; set; }
    
    string Name { get; set; }
    
    IReadOnlyCollection<ImageModel> Images { get; set; }
    
    public ItemType Type { get; }
}