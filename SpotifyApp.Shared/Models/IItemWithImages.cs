namespace SpotifyApp.Shared.Models;

public interface IItemWithImages
{
    IReadOnlyCollection<ImageModel> Images { get; set; }
}