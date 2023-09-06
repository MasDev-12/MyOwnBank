using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyOwnBank.Features.Authentification.Domain;

namespace MyOwnBank.Database.EntityConfiguration.Features.Authentification;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.HasKey(refreshToken => refreshToken.Id);
        builder.Property(refreshToken => refreshToken.Id).HasColumnName("Id").UseIdentityAlwaysColumn();
        builder.Property(refreshToken => refreshToken.Token).HasColumnName("Token").IsRequired();
        builder.Property(refreshToken => refreshToken.CreatedAt).HasColumnName("CreatedAt").IsRequired();
        builder.Property(refreshToken => refreshToken.TokenValidityPeriod).HasColumnName("ValidatyPeriod").IsRequired();
        builder.Property(refreshToken => refreshToken.TokenStoragePeriod).HasColumnName("StoragePeriod").IsRequired();
        builder.Property(refreshToken => refreshToken.Revoked).HasColumnName("Revoked").IsRequired();
        builder.Property(refreshToken => refreshToken.ReplacedByNextToken).HasColumnName("ReplacedByToken");
        builder.HasOne(refreshToken => refreshToken.User).WithMany(user => user.RefreshTokens).HasForeignKey(refreshToken => refreshToken.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}
