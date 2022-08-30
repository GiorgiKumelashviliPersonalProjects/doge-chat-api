using API.Source.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Source.Config.Database;

public class PermissionEntityConfiguration : IEntityTypeConfiguration<Permission>
{
    private const int MaxLength = 512;

    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder
            .HasOne(permission => permission.Role)
            .WithMany(role => role.Permissions)
            .HasForeignKey(permission => permission.RoleId)
            .IsRequired();

        builder
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAdd();

        builder
            .Property(entity => entity.ClaimType)
            .IsRequired()
            .HasMaxLength(MaxLength);

        builder
            .Property(entity => entity.ClaimValue)
            .IsRequired()
            .HasMaxLength(MaxLength);

        builder.ToTable("RoleClaims");
    }
}