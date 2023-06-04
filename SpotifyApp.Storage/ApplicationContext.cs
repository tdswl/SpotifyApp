using Microsoft.EntityFrameworkCore;
using SpotifyApp.Storage.Entities;
using SpotifyApp.Storage.OptionsFactories;

namespace SpotifyApp.Storage;

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
}