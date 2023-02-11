using Microsoft.EntityFrameworkCore;
using SpotifyApp.Storage.Entities;

namespace SpotifyApp.Storage;

public interface IApplicationContext
{
    public DbSet<UserSettings> UserSettings { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken token);
}