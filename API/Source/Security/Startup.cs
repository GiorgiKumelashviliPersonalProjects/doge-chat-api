using API.Source.Model;
using API.Source.Model.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace API.Source.Security;

public static class Startup
{
    public static IServiceCollection AddSecurityModule(this IServiceCollection serviceCollection)
    {
        // authorize policy missing
        serviceCollection
            .AddIdentityCore<User>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                
                // options.Password.RequireNonAlphanumeric = false;
                // options.Password.RequireDigit = false;
                // options.Password.RequireUppercase = false;
                // options.User.AllowedUserNameCharacters = null;
            })
            .AddRoles<Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddSignInManager<SignInManager<User>>()
            .AddRoleValidator<RoleValidator<Role>>()
            .AddEntityFrameworkStores<DataContext>();

        return serviceCollection;
    }
}