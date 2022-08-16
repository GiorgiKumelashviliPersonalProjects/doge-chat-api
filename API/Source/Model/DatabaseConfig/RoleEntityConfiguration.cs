using API.Source.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Source.Model.Config;

public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
{
    private const int MaxLength = 512;

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .Property(role => role.Description)
            .IsRequired()
            .HasMaxLength(MaxLength);

        builder
            .Property(role => role.CreatedAt)
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAdd();

        builder
            .HasMany(role => role.UserRoles)
            .WithOne(userRole => userRole.Role)
            .HasForeignKey(userRole => userRole.RoleId)
            .IsRequired();

        builder
            .HasMany(role => role.Permissions)
            .WithOne(permission => permission.Role)
            .HasForeignKey(role => role.RoleId)
            .IsRequired();

        builder.ToTable("Roles");
    }
}