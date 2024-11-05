using SpotifyApp.Repositories.Contracts;
using SpotifyApp.Storage.Contracts.Interfaces;
using SpotifyApp.Storage.Contracts.Models;

namespace SpotifyApp.Repositories;

internal class IoAuthTokenWriteRepository : WriterRepository<OAuthToken>, IOAuthTokenWriteRepository
{
    public IoAuthTokenWriteRepository(IStorageWriter writer) : base(writer)
    {
    }
}