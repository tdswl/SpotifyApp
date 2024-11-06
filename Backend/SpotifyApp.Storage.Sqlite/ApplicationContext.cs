using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

        DateTimeOffsetSupport(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(IApplicationContext).Assembly);
    }

    public DbSet<OAuthToken> OAuthTokens { get; set; }

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

    async Task IStorageInitialization.InitializeStorageAsync(CancellationToken cancellationToken)
    {
        await Database.OpenConnectionAsync(cancellationToken);
        await Database.EnsureCreatedAsync(cancellationToken);
        await Database.CloseConnectionAsync();
    }

    void IStorageInitialization.InitializeStorage()
    {
        Database.OpenConnection();
        Database.EnsureCreated();
        Database.CloseConnection();
    }

    private void DateTimeOffsetSupport(ModelBuilder builder)
    {
        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
        {
            // SQLite does not have proper support for DateTimeOffset via Entity Framework Core, see the limitations
            // here: https://docs.microsoft.com/en-us/ef/core/providers/sqlite/limitations#query-limitations
            // To work around this, when the Sqlite database provider is used, all model properties of type DateTimeOffset
            // use the DateTimeOffsetToBinaryConverter
            // Based on: https://github.com/aspnet/EntityFrameworkCore/issues/10784#issuecomment-415769754
            // This only supports millisecond precision, but should be sufficient for most use cases.
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTimeOffset)
                                                                               || p.PropertyType ==
                                                                               typeof(DateTimeOffset?));
                foreach (var property in properties)
                {
                    builder
                        .Entity(entityType.Name)
                        .Property(property.Name)
                        .HasConversion(new DateTimeOffsetToBinaryConverter());
                }
            }
        }
    }
}