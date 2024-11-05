using Microsoft.EntityFrameworkCore;

namespace SpotifyApp.Storage.Sqlite.OptionsFactories;

public interface IContextOptionsFactory
{
    DbContextOptions CreateOptions();
}