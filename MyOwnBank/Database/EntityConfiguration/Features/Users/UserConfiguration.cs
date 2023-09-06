using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyOwnBank.Features.Users.Domain;

namespace MyOwnBank.Database.EntityConfiguration.Features.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Id).HasColumnName("Id").UseIdentityAlwaysColumn();
        builder.Property(user => user.Name).HasColumnName("Name").IsRequired();
        builder.Property(user => user.LastName).HasColumnName("LastName").IsRequired();
        builder.Property(user => user.FatherName).HasColumnName("FatherName");
        builder.Property(user => user.Password).HasColumnName("Password").IsRequired();
        builder.Property(user => user.EmailPasswordHash).HasColumnName("EmailPasswordHash").IsRequired();
        builder.Property(user => user.IIN).HasColumnName("IIN").IsRequired();
        builder.HasIndex(user => user.IIN).IsUnique();
        builder.Property(user => user.PhoneNumber).HasColumnName("Phonenumber").IsRequired();
        builder.Property(user => user.EmailAddress).HasColumnName("EmailAddress").IsRequired();
        builder.Property(user => user.Confirm).HasColumnName("Confirm").IsRequired();
        builder.Property(user => user.ConfirmToken).HasColumnName("ConfirmToken").IsRequired();
        builder.Property(user => user.CreatedAt).HasColumnName("CreatedAt").IsRequired();
        builder.Property(user => user.UpdateAt).HasColumnName("UpdatedAt");
        builder.HasOne(user => user.UserProfile).WithOne(userP => userP.User).HasForeignKey<UserProfile>(userP => userP.UserId);
    }
}
