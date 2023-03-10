using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace SpotifyApp.Storage.OptionsFactories;

internal sealed class SqliteContextOptionsFactory : IContextOptionsFactory
{
    DbContextOptions IContextOptionsFactory.CreateOptions()
    {
        var connectionString = new SqliteConnectionStringBuilder("Data Source=App.db")
        {
            Mode = SqliteOpenMode.ReadWriteCreate,
            Password = "test",
        }.ToString();
        var builder = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlite(connectionString);

        return builder.Options;
    }
}