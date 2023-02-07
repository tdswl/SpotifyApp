namespace SpotifyApp.Shared.Models;

/// <summary>
/// Authorization information for requests
/// </summary>
public sealed class AuthorizationInfoModel
{
    public string? AccessToken { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public DateTimeOffset? AccessTokenExpiration  { get; set; }
    
    public DateTimeOffset? AuthenticationTime { get; set; }
}