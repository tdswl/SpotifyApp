using IdentityModel.Jwk;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SpotifyApp.Shared.Constants;
using SpotifyApp.Shared.Models;
using SpotifyApp.Storage;
using SpotifyApp.Storage.Entities;

namespace SpotifyApp.Shared.Services;

internal class AuthService : IAuthService
{
    private readonly IApplicationContext _applicationContext;
    private readonly IMemoryCache _memoryCache;
    private readonly IBrowser _browser;
    private readonly ILoggerFactory _loggerFactory;
    
    public AuthService(IApplicationContext applicationContext,
        IMemoryCache memoryCache,
        IBrowser browser,
        ILoggerFactory loggerFactory)
    {
        _applicationContext = applicationContext;
        _memoryCache = memoryCache;
        _browser = browser;
        _loggerFactory = loggerFactory;
    }
    
    public async Task<AuthorizationInfoModel> Login(CancellationToken token)
    {
        // Get from cache or database
        var info = GetInfoFromCache() ?? await GetInfoFromDatabase(token);

        // If found and token is expired - refresh
        if (info?.RefreshToken != null &&
            (info.AccessToken == null || info.AccessTokenExpiration < DateTimeOffset.UtcNow))
        {
            info = await GetInfoByRefreshToken(info.RefreshToken, token);
        }
        
        // Else do a new login
        info ??= await NewLogin(token);

        await AddOrUpdateInfoInStorage(info, token);
        _memoryCache.Set(MemoryCacheKeys.AuthorizationInfo, info);
        
        return info;
    }
    
    public async Task<AuthorizationInfoModel> RefreshToken(string refreshToken, CancellationToken token)
    {
        var info = await GetInfoByRefreshToken(refreshToken, token);

        await AddOrUpdateInfoInStorage(info, token);
        _memoryCache.Set(MemoryCacheKeys.AuthorizationInfo, info);

        return info;
    }

    private OidcClientOptions GenerateOptions()
    {
       return new OidcClientOptions
        {
            Authority = ApplicationSettings.Authority,
            ClientId = ApplicationSettings.ClientId,
            RedirectUri = ApplicationSettings.RedirectUri,
            Scope = "user-read-private user-read-email user-library-read user-top-read",
            Browser = _browser,
            ProviderInformation = new ProviderInformation
            {
                IssuerName = "https://accounts.spotify.com/",
                AuthorizeEndpoint = "https://accounts.spotify.com/authorize",
                TokenEndpoint = "https://accounts.spotify.com/api/token",
                UserInfoEndpoint = "https://api.spotify.com/v1/me",
                KeySet = new JsonWebKeySet(),
            },
            LoadProfile = false,
            LoggerFactory = _loggerFactory,
        };
    }

    private AuthorizationInfoModel? GetInfoFromCache()
    {
        if (_memoryCache.TryGetValue(MemoryCacheKeys.AuthorizationInfo, out AuthorizationInfoModel? value))
        {
            return value;
        }

        return null;
    }
    
    private async Task<AuthorizationInfoModel?> GetInfoFromDatabase(CancellationToken cancellationToken)
    {
        var latestUser = await _applicationContext.UserSettings.FirstOrDefaultAsync(cancellationToken);
        if (latestUser == null)
        {
            return null;
        }

        return new AuthorizationInfoModel
        {
            RefreshToken = latestUser.RefreshToken,
            AccessToken = latestUser.AccessToken,
            AccessTokenExpiration = latestUser.AccessTokenExpiration,
        };
    }

    private async Task<AuthorizationInfoModel> GetInfoByRefreshToken(string refreshToken, CancellationToken token)
    {
        var options = GenerateOptions();
        var oidcClient = new OidcClient(options);
        var result = await oidcClient.RefreshTokenAsync(refreshToken, cancellationToken: token);
        
        return  new AuthorizationInfoModel
        {
            RefreshToken = result.RefreshToken,
            AccessToken = result.AccessToken,
            AccessTokenExpiration = result.AccessTokenExpiration,
        };
    }

    private async Task<AuthorizationInfoModel> NewLogin(CancellationToken cancellationToken)
    {
        var options = GenerateOptions();
        var oidcClient = new OidcClient(options);
        var result = await oidcClient.LoginAsync(cancellationToken: cancellationToken);
            
        return new AuthorizationInfoModel
        {
            RefreshToken = result.RefreshToken,
            AccessToken = result.AccessToken,
            AccessTokenExpiration = result.AccessTokenExpiration,
            AuthenticationTime = result.AuthenticationTime,
        };
    }
  
    private async Task AddOrUpdateInfoInStorage(AuthorizationInfoModel infoModel, CancellationToken token)
    {
        var userSettings = await _applicationContext.UserSettings.FirstOrDefaultAsync(token);
        if (userSettings != null)
        {
            userSettings.AuthenticationTime = infoModel.AuthenticationTime;
            userSettings.AccessToken = infoModel.AccessToken;
            userSettings.RefreshToken = infoModel.RefreshToken;
            userSettings.AccessTokenExpiration = infoModel.AccessTokenExpiration;
            
            _applicationContext.UserSettings.Update(userSettings);
            await _applicationContext.SaveChangesAsync(token);
        }
        else
        {
            userSettings = new AppUserSettings
            {
                Id = Guid.NewGuid(),
                AccessToken = infoModel.AccessToken,
                AccessTokenExpiration = infoModel.AccessTokenExpiration,
                RefreshToken = infoModel.RefreshToken,
                AuthenticationTime = infoModel.AuthenticationTime
            };
            
            await _applicationContext.UserSettings.AddAsync(userSettings, token);
            await _applicationContext.SaveChangesAsync(token);
        }
    }
}