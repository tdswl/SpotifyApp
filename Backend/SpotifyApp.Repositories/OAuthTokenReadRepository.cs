using Microsoft.EntityFrameworkCore;
using SpotifyApp.Repositories.Contracts;
using SpotifyApp.Storage.Contracts.Interfaces;
using SpotifyApp.Storage.Contracts.Models;

namespace SpotifyApp.Repositories;

internal sealed class OAuthTokenReadRepository : IOAuthTokenReadRepository
{
    private readonly IStorageReader _reader;

    public OAuthTokenReadRepository(IStorageReader reader)
    {
        _reader = reader;
    }

    Task<OAuthToken?> IOAuthTokenReadRepository.GetLatestUserToken(CancellationToken cancellationToken)
    {
        return _reader.Read<OAuthToken>()
            .OrderByDescending(a => a.AuthenticationTime)
            .FirstOrDefaultAsync(cancellationToken);
    }
}