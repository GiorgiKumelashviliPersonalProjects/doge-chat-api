using API.Source.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Source.Config.Database;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    private const int MaxLength = 512;

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasMany(user => user.UserRoles)
            .WithOne(userRole => userRole.User)
            .HasForeignKey(userRole => userRole.UserId)
            .IsRequired();

        builder
            .Property(entity => entity.FirstName)
            .IsRequired()
            .HasMaxLength(MaxLength);

        builder
            .Property(entity => entity.LastName)
            .IsRequired()
            .HasMaxLength(MaxLength);

        builder
            .Property(entity => entity.Email)
            .IsRequired()
            .HasMaxLength(MaxLength);

        builder
            .Property(entity => entity.IsOnline)
            .HasDefaultValue(false);

        builder
            .Property(entity => entity.UserName)
            .IsRequired()
            .HasMaxLength(MaxLength);

        builder
            .Property(entity => entity.CreatedAt)
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAdd();

        builder
            .Property(entity => entity.UpdatedAt)
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAddOrUpdate();

        builder.ToTable("Users");
    }
}