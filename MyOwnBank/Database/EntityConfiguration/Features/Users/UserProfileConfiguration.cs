using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyOwnBank.Features.Users.Domain;

namespace MyOwnBank.Database.EntityConfiguration.Features.Users;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("UsersProfile");
        builder.HasKey("Id");
        builder.Property(userP => userP.Id).HasColumnName("Id").UseIdentityAlwaysColumn();
        builder.Property(userP => userP.CodeWord).HasColumnName("CodeWord").IsRequired();
        builder.Property(userP => userP.Age).HasColumnName("Age").IsRequired();
    }
}
