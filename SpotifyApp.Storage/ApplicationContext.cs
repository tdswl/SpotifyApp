using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SpotifyApp.Storage.Entities;

namespace SpotifyApp.Storage;

public sealed class ApplicationContext : DbContext, IApplicationContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = new SqliteConnectionStringBuilder("Data Source=App.db")
        {
            Mode = SqliteOpenMode.ReadWriteCreate,
            Password = "test",
        }.ToString();
        
        optionsBuilder.UseSqlite(connectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(IApplicationContext).Assembly);
    }

    public DbSet<AppUserSettings> UserSettings { get; set; }
}