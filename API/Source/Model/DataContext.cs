#pragma warning disable CS8618

using System.Reflection;
using API.Source.Model.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Source.Model;

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
    }
}