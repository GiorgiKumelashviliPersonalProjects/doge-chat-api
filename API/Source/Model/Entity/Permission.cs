using Microsoft.AspNetCore.Identity;

namespace API.Source.Model.Entity;

public class Permission : IdentityRoleClaim<long>
{
    public string? Description { get; set; }
    public Role Role { get; set; }
    public DateTime CreatedAt { get; set; }
}