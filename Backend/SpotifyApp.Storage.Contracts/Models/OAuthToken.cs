using SpotifyApp.Storage.Contracts.Interfaces;

namespace SpotifyApp.Storage.Contracts.Models;

/// <summary>
/// Application user
/// </summary>
public sealed class OAuthToken : IStorageEntity
{
    public Guid Id { get; set; }
    
    public string? AccessToken { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public DateTimeOffset? AccessTokenExpiration  { get; set; }
    
    public DateTimeOffset? AuthenticationTime { get; set; }
}