// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

#pragma warning disable CS8618

using System.Reflection;
using API.Source.Model.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Source.Config;

public class DataContext : IdentityDbContext<
    User, Role, long,
    IdentityUserClaim<long>, UserRole,
    IdentityUserLogin<long>, Permission, IdentityUserToken<long>>
{
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RequestSignup> RequestSignups { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.UseSerialColumns();
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ApplyUtcDateTimeConverter();
    }
}

public static class UtcDateAnnotation
{
    private const string IsUtcAnnotation = "IsUtc";

    private static readonly ValueConverter<DateTime, DateTime> UtcConverter = new(
        v => v,
        v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
    );

    private static bool IsUtc(this IReadOnlyAnnotatable property) =>
        (bool?)property.FindAnnotation(IsUtcAnnotation)?.Value ?? true;

    public static void ApplyUtcDateTimeConverter(this ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (!property.IsUtc())
                {
                    continue;
                }

                if (property.ClrType == typeof(DateTime) ||
                    property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(UtcConverter);
                }
            }
        }
    }
}