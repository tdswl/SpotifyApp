using SpotifyApp.Shared.Models;

namespace SpotifyApp.Shared.Services;

public interface IAuthService
{
    public Task<AuthorizationInfoModel> Login(CancellationToken token);
}