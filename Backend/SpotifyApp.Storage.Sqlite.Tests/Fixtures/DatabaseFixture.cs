using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpotifyApp.Storage.Sqlite;
using SpotifyApp.Storage.Sqlite.OptionsFactories;

namespace SpotifyApp.Storage.SQLite.Tests.Fixtures;

public sealed class DatabaseFixture : IAsyncLifetime
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    public IApplicationContext Database;

    public async Task InitializeAsync()
    {
        var context = new ApplicationContext(new SqliteInMemoryContextOptionsFactory(LoggerFactory.Create(builder => { builder.AddConsole(); })));
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