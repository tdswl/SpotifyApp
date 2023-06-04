using SpotifyApp.Api.Client.OpenApiClient;

namespace SpotifyApp.Shared.Services;

public sealed class TokenService : ITokenService
{
    private readonly IAuthService _authService;

    public TokenService(IAuthService authService)
    {
        _authService = authService;
    }
    
    public async Task<string> GetAccessToken(CancellationToken token)
    {
        var loginInfo = await _authService.Login(token);
        return loginInfo.AccessToken;
    }
}