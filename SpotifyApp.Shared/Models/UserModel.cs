namespace SpotifyApp.Shared.Models;

public sealed class UserModel : IItemWithImages
{
    public required string Id { get; set; }
    
    public required string DisplayName { get; set; }

    public required IReadOnlyCollection<ImageModel> Images { get; set; }
}