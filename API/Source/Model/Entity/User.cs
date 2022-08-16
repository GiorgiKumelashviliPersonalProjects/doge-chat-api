using API.Source.Model.Enum;
using Microsoft.AspNetCore.Identity;

namespace API.Source.Model.Entity;

public class User : IdentityUser<long>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public Gender Gender { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }

    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}