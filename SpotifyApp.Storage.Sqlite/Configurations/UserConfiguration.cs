using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotifyApp.Storage.Contracts.Models;

namespace SpotifyApp.Storage.Sqlite.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<UserSettings>
{
    public void Configure(EntityTypeBuilder<UserSettings> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).IsRequired();
    }
}