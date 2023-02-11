using Microsoft.EntityFrameworkCore;

namespace SpotifyApp.Storage.OptionsFactories;

public interface IContextOptionsFactory
{
    DbContextOptions CreateOptions();
}