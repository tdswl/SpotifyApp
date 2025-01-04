using Duende.IdentityModel.OidcClient;
using Duende.IdentityModel.OidcClient.Results;

namespace SpotifyApp.Api.Client.Auth;

public interface IAuthClient
{
    public Task<LoginResult> Login(CancellationToken token);
    
    public Task<RefreshTokenResult> Refresh(string refreshToken, CancellationToken token);
}