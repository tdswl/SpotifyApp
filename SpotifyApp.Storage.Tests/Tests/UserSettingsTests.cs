using FluentAssertions;
using SpotifyApp.Storage.Entities;
using SpotifyApp.Storage.Tests.Fixtures;

namespace SpotifyApp.Storage.Tests.Tests;

[Collection(nameof(StorageCollection))]
public sealed class UserSettingsTests : IDisposable
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly IApplicationContext _context;

    public UserSettingsTests(DatabaseFixture fixture)
    {
        _context = fixture.Database;
    }

    [Fact]
    public async Task Add_Succeed()
    {
        // arrange
        var user = new UserSettings
        {
            Id = Guid.NewGuid(),
            AccessToken = "at",
            AuthenticationTime = DateTimeOffset.UtcNow,
            RefreshToken = "rt",
            AccessTokenExpiration = DateTimeOffset.UtcNow,
        };

        // act
        await _context.UserSettings.AddAsync(user, _cancellationTokenSource.Token);
        await _context.SaveChangesAsync(_cancellationTokenSource.Token);

        // assert
        _context.UserSettings.Should().ContainSingle(a => a.Id == user.Id);
    }

    public void Dispose()
    {
        _cancellationTokenSource.Dispose();
    }
}