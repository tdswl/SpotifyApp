using SpotifyApp.Services.Contracts.Models;

namespace SpotifyApp.Services.Contracts;

public interface IAuthService
{
    public Task<AuthorizationInfoModel> Login(CancellationToken token);
}