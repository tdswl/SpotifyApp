using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SpotifyApp.Storage.Sqlite.OptionsFactories;

internal sealed class SqliteContextOptionsFactory : IContextOptionsFactory
{
    private readonly ILoggerFactory _loggerFactory;

    public SqliteContextOptionsFactory(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }

    DbContextOptions IContextOptionsFactory.CreateOptions()
    {
        var connectionString = new SqliteConnectionStringBuilder("Data Source=App.db")
        {
            Mode = SqliteOpenMode.ReadWriteCreate,
            Password = "test",
        }.ToString();

        var builder = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlite(connectionString)
            .UseLoggerFactory(_loggerFactory);

        return builder.Options;
    }
}