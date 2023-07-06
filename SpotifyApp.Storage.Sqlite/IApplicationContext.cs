using Microsoft.EntityFrameworkCore;
using SpotifyApp.Storage.Contracts.Interfaces;
using SpotifyApp.Storage.Contracts.Models;

namespace SpotifyApp.Storage.Sqlite;

public interface IApplicationContext : IStorageReader, IStorageWriter, IStorageInitialization
{
    DbSet<UserSettings> UserSettings { get; set; }
}