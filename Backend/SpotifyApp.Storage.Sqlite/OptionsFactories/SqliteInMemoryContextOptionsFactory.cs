using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SpotifyApp.Storage.Sqlite.OptionsFactories;

internal sealed class SqliteInMemoryContextOptionsFactory : IContextOptionsFactory
{
    private readonly ILoggerFactory _loggerFactory;

    public SqliteInMemoryContextOptionsFactory(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }
    
    DbContextOptions IContextOptionsFactory.CreateOptions()
    {
        var connectionString = new SqliteConnectionStringBuilder("Data Source=:memory:")
        {
            Mode = SqliteOpenMode.ReadWriteCreate,
            Password = "test",
        }.ToString();
        var builder = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlite(connectionString);
        
#if DEBUG
        builder = builder.UseLoggerFactory(_loggerFactory);
#endif

        return builder.Options;
    }
}