using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace SpotifyApp.Storage.Sqlite.OptionsFactories;

internal sealed class SqliteInMemoryContextOptionsFactory : IContextOptionsFactory
{
    DbContextOptions IContextOptionsFactory.CreateOptions()
    {
        var connectionString = new SqliteConnectionStringBuilder("Data Source=:memory:")
        {
            Mode = SqliteOpenMode.ReadWriteCreate,
            Password = "test",
        }.ToString();
        var builder = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlite(connectionString);

        return builder.Options;
    }
}