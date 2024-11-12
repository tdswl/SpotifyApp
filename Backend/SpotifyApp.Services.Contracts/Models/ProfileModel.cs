namespace SpotifyApp.Services.Contracts.Models;

public sealed class ProfileModel
{
    public string? ProfileImageUrl { get; set; }
    
    public required string UserName { get; set; }
}