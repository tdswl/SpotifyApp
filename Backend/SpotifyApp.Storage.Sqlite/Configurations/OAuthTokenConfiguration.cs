using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotifyApp.Storage.Contracts.Models;

namespace SpotifyApp.Storage.Sqlite.Configurations;

internal sealed class OAuthTokenConfiguration : IEntityTypeConfiguration<OAuthToken>
{
    public void Configure(EntityTypeBuilder<OAuthToken> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).IsRequired();
    }
}