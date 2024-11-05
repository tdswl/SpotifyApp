using AsyncKeyedLock;
using Microsoft.Extensions.Caching.Memory;
using SpotifyApp.Api.Client.Auth;
using SpotifyApp.Repositories.Contracts;
using SpotifyApp.Shared.Constants;
using SpotifyApp.Shared.Models;
using SpotifyApp.Storage.Contracts.Models;

namespace SpotifyApp.Shared.Services;

internal sealed class AuthService : IAuthService
{
    private static readonly AsyncNonKeyedLocker Semaphore = new(1);
    
    private readonly IOAuthTokenReadRepository _ioAuthTokenReadRepository;
    private readonly IOAuthTokenWriteRepository _ioAuthTokenWriteRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly IAuthClient _authClient;
    
    public AuthService(IMemoryCache memoryCache,
        IAuthClient authClient, 
        IOAuthTokenReadRepository ioAuthTokenReadRepository, 
        IOAuthTokenWriteRepository ioAuthTokenWriteRepository)
    {
        _memoryCache = memoryCache;
        _authClient = authClient;
        _ioAuthTokenReadRepository = ioAuthTokenReadRepository;
        _ioAuthTokenWriteRepository = ioAuthTokenWriteRepository;
    }
    
    async Task<AuthorizationInfoModel> IAuthService.Login(CancellationToken token)
    {
        // lock to prevent many login requests to Spotify
        using (await Semaphore.LockAsync(token).ConfigureAwait(false))
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
        var latestUser = await _ioAuthTokenReadRepository.GetLatestUserToken(cancellationToken);
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
        var userSettings = await _ioAuthTokenReadRepository.GetLatestUserToken(token);
        if (userSettings != null)
        {
            userSettings.AuthenticationTime = infoModel.AuthenticationTime;
            userSettings.AccessToken = infoModel.AccessToken;
            userSettings.RefreshToken = infoModel.RefreshToken;
            userSettings.AccessTokenExpiration = infoModel.AccessTokenExpiration;
            
            _ioAuthTokenWriteRepository.Update(userSettings);
            await _ioAuthTokenWriteRepository.SaveChangesAsync(token);
        }
        else
        {
            userSettings = new OAuthToken
            {
                Id = Guid.NewGuid(),
                AccessToken = infoModel.AccessToken,
                AccessTokenExpiration = infoModel.AccessTokenExpiration,
                RefreshToken = infoModel.RefreshToken,
                AuthenticationTime = infoModel.AuthenticationTime
            };
            
            _ioAuthTokenWriteRepository.Add(userSettings);
            await _ioAuthTokenWriteRepository.SaveChangesAsync(token);
        }
    }
}