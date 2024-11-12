using SpotifyApp.Services.Contracts.Models;

namespace SpotifyApp.Services.Contracts;

public interface IProfileService
{
    public Task<ProfileModel> GetCurrentUserProfile(CancellationToken cancellationToken);
}