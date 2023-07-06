using Microsoft.EntityFrameworkCore;
using SpotifyApp.Storage.Contracts.Interfaces;
using SpotifyApp.Storage.Contracts.Models;
using SpotifyApp.Storage.Sqlite.OptionsFactories;

namespace SpotifyApp.Storage.Sqlite;

internal sealed class ApplicationContext : DbContext, IApplicationContext
{
    public ApplicationContext(IContextOptionsFactory optionsFactory)
        : base(optionsFactory.CreateOptions())
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(IApplicationContext).Assembly);
    }

    public DbSet<UserSettings> UserSettings { get; set; }
    
    IQueryable<TEntity> IStorageReader.Read<TEntity>()
    {
        return Set<TEntity>().AsNoTracking().AsQueryable();
    }

    void IStorageWriter.Add<TEntity>(TEntity entity)
    {
        Entry(entity).State = EntityState.Added;
    }

    void IStorageWriter.Update<TEntity>(TEntity entity)
    {
        Entry(entity).State = EntityState.Modified;
    }

    void IStorageWriter.Delete<TEntity>(TEntity entity)
    {
        Entry(entity).State = EntityState.Deleted;
    }

    public async Task InitializeStorageAsync(CancellationToken cancellationToken)
    {
        await Database.OpenConnectionAsync(cancellationToken);
        await Database.EnsureCreatedAsync(cancellationToken);
        await Database.CloseConnectionAsync();
    }

    public void InitializeStorage()
    {
        Database.OpenConnection();
        Database.EnsureCreated();
        Database.CloseConnection();
    }
}