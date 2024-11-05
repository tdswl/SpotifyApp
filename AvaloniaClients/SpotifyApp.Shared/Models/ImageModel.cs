namespace SpotifyApp.Shared.Models;

public sealed class ImageModel
{
    public required string Url { get; set; }
    
    public int? Height { get; set; }
    
    public int? Width { get; set; }
}