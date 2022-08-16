using API.Source.Modules.User.Interfaces;
using API.Source.Modules.User.RefreshToken;

namespace API.Source.Modules.User;

public static class Startup
{
    public static IServiceCollection AddUserModule(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        return serviceCollection;
    }
}