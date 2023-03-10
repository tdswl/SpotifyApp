using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotifyApp.Storage.Entities;

namespace SpotifyApp.Storage.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<UserSettings>
{
    public void Configure(EntityTypeBuilder<UserSettings> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).IsRequired();
    }
}