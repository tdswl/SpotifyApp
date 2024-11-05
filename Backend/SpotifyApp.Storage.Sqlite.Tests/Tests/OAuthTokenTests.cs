using FluentAssertions;
using SpotifyApp.Storage.Contracts.Models;
using SpotifyApp.Storage.Sqlite;
using SpotifyApp.Storage.SQLite.Tests.Fixtures;

namespace SpotifyApp.Storage.SQLite.Tests.Tests;

[Collection(nameof(StorageCollection))]
public sealed class OAuthTokenTests : IDisposable
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly IApplicationContext _context;

    public OAuthTokenTests(DatabaseFixture fixture)
    {
        _context = fixture.Database;
    }

    [Fact]
    public async Task Add_Succeed()
    {
        // arrange
        var user = new OAuthToken
        {
            Id = Guid.NewGuid(),
            AccessToken = "at",
            AuthenticationTime = DateTimeOffset.UtcNow,
            RefreshToken = "rt",
            AccessTokenExpiration = DateTimeOffset.UtcNow,
        };

        // act
        await _context.OAuthTokens.AddAsync(user, _cancellationTokenSource.Token);
        await _context.SaveChangesAsync(_cancellationTokenSource.Token);

        // assert
        _context.OAuthTokens.Should().ContainSingle(a => a.Id == user.Id);
    }

    public void Dispose()
    {
        _cancellationTokenSource.Dispose();
    }
}