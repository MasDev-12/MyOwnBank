using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyOwnBank.Features.Users.Domain;

namespace MyOwnBank.Database.EntityConfiguration.Features.Users;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        builder.HasKey(role => role.Id);
        builder.Property(role => role.Id).HasColumnName("Id").UseIdentityAlwaysColumn();
        builder.Property(role => role.UserRoleCode).HasColumnName("UserRoleCode").IsRequired();
        builder.Property(role => role.RoleName).HasColumnName("RoleName").IsRequired();
        builder.Property(role => role.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(role => role.CreatedAt).HasColumnName("CreatedAt").IsRequired();
        builder.HasOne(role => role.User).WithMany(user => user.Roles).HasForeignKey(role => role.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}
