using SpotifyApp.Repositories.Contracts;
using SpotifyApp.Storage.Contracts.Interfaces;
using SpotifyApp.Storage.Contracts.Models;

namespace SpotifyApp.Repositories;

internal sealed class OAuthTokenWriteRepository : WriterRepository<OAuthToken>, IOAuthTokenWriteRepository
{
    public OAuthTokenWriteRepository(IStorageWriter writer) : base(writer)
    {
    }
}