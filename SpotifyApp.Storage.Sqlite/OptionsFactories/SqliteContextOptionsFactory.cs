using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SpotifyApp.Storage.Sqlite.OptionsFactories;

internal sealed class SqliteContextOptionsFactory : IContextOptionsFactory
{
#if DEBUG
    private static readonly ILoggerFactory ConsoleLoggerFactory =
        LoggerFactory.Create(builder => { builder.AddConsole(); });
#endif

    DbContextOptions IContextOptionsFactory.CreateOptions()
    {
        var connectionString = new SqliteConnectionStringBuilder("Data Source=App.db")
        {
            Mode = SqliteOpenMode.ReadWriteCreate,
            Password = "test",
        }.ToString();

        var builder = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlite(connectionString);

#if DEBUG
        builder = builder.UseLoggerFactory(ConsoleLoggerFactory);
#endif

        return builder.Options;
    }
}