using Microsoft.EntityFrameworkCore;
using SpotifyApp.Repositories.Contracts;
using SpotifyApp.Storage.Contracts.Interfaces;
using SpotifyApp.Storage.Contracts.Models;

namespace SpotifyApp.Repositories;

internal class UserSettingsReadRepository : IUserSettingsReadRepository
{
    private readonly IStorageReader _reader;

    public UserSettingsReadRepository(IStorageReader reader)
    {
        _reader = reader;
    }

    Task<UserSettings?> IUserSettingsReadRepository.GetUserSettings(CancellationToken cancellationToken)
    {
        return _reader.Read<UserSettings>().FirstOrDefaultAsync(cancellationToken);
    }
}