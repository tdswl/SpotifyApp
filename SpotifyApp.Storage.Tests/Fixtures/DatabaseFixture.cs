using Microsoft.EntityFrameworkCore;
using SpotifyApp.Storage.OptionsFactories;

namespace SpotifyApp.Storage.Tests.Fixtures;

public sealed class DatabaseFixture : IAsyncLifetime
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    public IApplicationContext Database;

    public async Task InitializeAsync()
    {
        var context = new ApplicationContext(new SqliteInMemoryContextOptionsFactory());
        await context.Database.OpenConnectionAsync(_cancellationTokenSource.Token);
        await context.Database.EnsureCreatedAsync(_cancellationTokenSource.Token);
        Database = context;
    }

    public async Task DisposeAsync()
    {
        if (Database is DbContext context)
        {
            await context.Database.EnsureDeletedAsync(_cancellationTokenSource.Token);
            await context.DisposeAsync();
        }
    }
}