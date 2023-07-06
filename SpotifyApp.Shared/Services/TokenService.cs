using SpotifyApp.Api.Client.OpenApiClient;

namespace SpotifyApp.Shared.Services;

internal sealed class TokenService : ITokenService
{
    private readonly IAuthService _authService;

    public TokenService(IAuthService authService)
    {
        _authService = authService;
    }
    
    async Task<string> ITokenService.GetAccessToken(CancellationToken token)
    {
        var loginInfo = await _authService.Login(token);
        return loginInfo.AccessToken;
    }
}