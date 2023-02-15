using IdentityModel.Jwk;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using IdentityModel.OidcClient.Results;
using Microsoft.Extensions.Logging;
using SpotifyApp.Api.Client.Users;
using SpotifyApp.Api.Contracts.Auth;

namespace SpotifyApp.Api.Client.Auth;

internal class AuthClient : IAuthClient
{
    private readonly IBrowser _browser;
    private readonly ILoggerFactory _loggerFactory;
    private readonly IOidcConfiguration _oidcConfiguration;

    public AuthClient(IBrowser browser,
        ILoggerFactory loggerFactory,
        IOidcConfiguration oidcConfiguration)
    {
        _browser = browser;
        _loggerFactory = loggerFactory;
        _oidcConfiguration = oidcConfiguration;
    }
    
    public Task<LoginResult> Login(CancellationToken token)
    {
        var options = GenerateClientOptions();
        var oidcClient = new OidcClient(options);
        return oidcClient.LoginAsync(cancellationToken: token);
    }

    public Task<RefreshTokenResult> Refresh(string refreshToken, CancellationToken token)
    {
        var options = GenerateClientOptions();
        var oidcClient = new OidcClient(options);
        return oidcClient.RefreshTokenAsync(refreshToken, cancellationToken: token);
    }
    
    private OidcClientOptions GenerateClientOptions()
    {
        return new OidcClientOptions
        {
            Authority =  AuthRoutes.IssuerName,
            ClientId = _oidcConfiguration.ClientId,
            RedirectUri = _oidcConfiguration.RedirectUri,
            Scope =  _oidcConfiguration.Scope,
            Browser = _browser,
            ProviderInformation = new ProviderInformation
            {
                IssuerName = AuthRoutes.IssuerName,
                AuthorizeEndpoint = AuthRoutes.AuthorizeEndpoint,
                TokenEndpoint = AuthRoutes.TokenEndpoint,
                UserInfoEndpoint = UserRoutes.GetUsersProfile,
                KeySet = new JsonWebKeySet(),
            },
            LoadProfile = false,
            LoggerFactory = _loggerFactory,
        };
    }
}