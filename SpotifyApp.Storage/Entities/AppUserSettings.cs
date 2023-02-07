namespace SpotifyApp.Storage.Entities;

/// <summary>
/// Application user
/// </summary>
public sealed class AppUserSettings
{
    public required Guid Id { get; set; }
    
    public string? AccessToken { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public DateTimeOffset? AccessTokenExpiration  { get; set; }
    
    public DateTimeOffset? AuthenticationTime { get; set; }
}