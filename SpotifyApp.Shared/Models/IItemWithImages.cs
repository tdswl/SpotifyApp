namespace SpotifyApp.Shared.Models;

public interface IItemWithImages
{
    IReadOnlyCollection<Image> Images { get; set; }
}