using Microsoft.Extensions.Caching.Memory;
using SpotifyApp.Api.Client.Auth;
using SpotifyApp.Repositories.Contracts;
using SpotifyApp.Shared.Constants;
using SpotifyApp.Shared.Models;
using SpotifyApp.Storage.Contracts.Models;

namespace SpotifyApp.Shared.Services;

internal sealed class AuthService : IAuthService
{
    private static readonly SemaphoreSlim Semaphore = new(1, 1);
    
    private readonly IUserSettingsReadRepository _userSettingsReadRepository;
    private readonly IUserSettingsWriteRepository _userSettingsWriteRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly IAuthClient _authClient;
    
    public AuthService(IMemoryCache memoryCache,
        IAuthClient authClient, 
        IUserSettingsReadRepository userSettingsReadRepository, 
        IUserSettingsWriteRepository userSettingsWriteRepository)
    {
        _memoryCache = memoryCache;
        _authClient = authClient;
        _userSettingsReadRepository = userSettingsReadRepository;
        _userSettingsWriteRepository = userSettingsWriteRepository;
    }
    
    async Task<AuthorizationInfoModel> IAuthService.Login(CancellationToken token)
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
        var latestUser = await _userSettingsReadRepository.GetUserSettings(cancellationToken);
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
        var userSettings = await _userSettingsReadRepository.GetUserSettings(token);
        if (userSettings != null)
        {
            userSettings.AuthenticationTime = infoModel.AuthenticationTime;
            userSettings.AccessToken = infoModel.AccessToken;
            userSettings.RefreshToken = infoModel.RefreshToken;
            userSettings.AccessTokenExpiration = infoModel.AccessTokenExpiration;
            
            _userSettingsWriteRepository.Update(userSettings);
            await _userSettingsWriteRepository.SaveChangesAsync(token);
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
            
            _userSettingsWriteRepository.Add(userSettings);
            await _userSettingsWriteRepository.SaveChangesAsync(token);
        }
    }
}