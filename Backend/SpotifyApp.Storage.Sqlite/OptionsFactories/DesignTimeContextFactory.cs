using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace SpotifyApp.Storage.Sqlite.OptionsFactories;

/// <summary>
/// For design time migrations
/// https://learn.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
/// </summary>
internal class DesignTimeContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    /// <inheritdoc cref="IDesignTimeDbContextFactory{TContext}.CreateDbContext"/>
    public ApplicationContext CreateDbContext(string[] args)
    {
        var contextConfiguration = new SqliteContextOptionsFactory(LoggerFactory.Create(builder => { builder.AddConsole(); }));

        return new ApplicationContext(contextConfiguration);
    }
}