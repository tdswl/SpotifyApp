using SpotifyApp.Repositories.Contracts;
using SpotifyApp.Storage.Contracts.Interfaces;
using SpotifyApp.Storage.Contracts.Models;

namespace SpotifyApp.Repositories;

internal class UserSettingsWriteRepository : WriterRepository<UserSettings>, IUserSettingsWriteRepository
{
    public UserSettingsWriteRepository(IStorageWriter writer) : base(writer)
    {
    }
}