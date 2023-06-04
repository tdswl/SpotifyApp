using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SpotifyApp.Api.Client.Auth;
using SpotifyApp.Shared.Constants;
using SpotifyApp.Shared.Models;
using SpotifyApp.Storage;
using SpotifyApp.Storage.Entities;

namespace SpotifyApp.Shared.Services;

internal sealed class AuthService : IAuthService
{
    private static readonly SemaphoreSlim Semaphore = new(1, 1);
    
    private readonly IApplicationContext _applicationContext;
    private readonly IMemoryCache _memoryCache;
    private readonly IAuthClient _authClient;
    
    public AuthService(IApplicationContext applicationContext,
        IMemoryCache memoryCache,
        IAuthClient authClient)
    {
        _applicationContext = applicationContext;
        _memoryCache = memoryCache;
        _authClient = authClient;
    }
    
    public async Task<AuthorizationInfoModel> Login(CancellationToken token)
    {
        // lock to prevent many login requests to Spotify
        await Semaphore.WaitAsync(token).ConfigureAwait(false);

        try
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

            _memoryCache.Set(MemoryCacheKeys.AuthorizationInfo, info);

            return info;
        }
        finally
        {
            Semaphore.Release();
        }
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
        var result =  await _authClient.Refresh(refreshToken, token);

        var info = new AuthorizationInfoModel
        {
            RefreshToken = result.RefreshToken,
            AccessToken = result.AccessToken,
            AccessTokenExpiration = result.AccessTokenExpiration,
        };
        
        await AddOrUpdateInfoInStorage(info, token);
        
        return info;
    }

    private async Task<AuthorizationInfoModel> NewLogin(CancellationToken token)
    {
        var result = await _authClient.Login(token);
        
        var info = new AuthorizationInfoModel
        {
            RefreshToken = result.RefreshToken,
            AccessToken = result.AccessToken,
            AccessTokenExpiration = result.AccessTokenExpiration,
            AuthenticationTime = result.AuthenticationTime,
        };
        
        await AddOrUpdateInfoInStorage(info, token);
        
        return info;
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
            userSettings = new UserSettings
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