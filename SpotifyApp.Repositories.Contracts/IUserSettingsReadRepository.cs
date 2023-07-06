using SpotifyApp.Storage.Contracts.Models;

namespace SpotifyApp.Repositories.Contracts;

public interface IUserSettingsReadRepository
{
    Task<UserSettings?> GetUserSettings(CancellationToken cancellationToken);
}