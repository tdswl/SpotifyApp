using SpotifyApp.Shared.Enums;

namespace SpotifyApp.Shared.Models;

public sealed class UserModel : IItemWithImages
{
    public string Id { get; set; }
    
    public string DisplayName { get; set; }

    public IReadOnlyCollection<Image> Images { get; set; }
}