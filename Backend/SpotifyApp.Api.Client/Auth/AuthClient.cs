using Duende.IdentityModel.Jwk;
using Duende.IdentityModel.OidcClient;
using Duende.IdentityModel.OidcClient.Browser;
using Duende.IdentityModel.OidcClient.Results;
using Microsoft.Extensions.Logging;

namespace SpotifyApp.Api.Client.Auth;

internal sealed class AuthClient : IAuthClient
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
    
    Task<LoginResult> IAuthClient.Login(CancellationToken token)
    {
        var options = GenerateClientOptions();
        var oidcClient = new OidcClient(options);
        return oidcClient.LoginAsync(cancellationToken: token);
    }

    Task<RefreshTokenResult> IAuthClient.Refresh(string refreshToken, CancellationToken token)
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
                UserInfoEndpoint = AuthRoutes.GetCurrentUserProfile,
                KeySet = new JsonWebKeySet(),
            },
            LoadProfile = false,
            LoggerFactory = _loggerFactory,
        };
    }
}