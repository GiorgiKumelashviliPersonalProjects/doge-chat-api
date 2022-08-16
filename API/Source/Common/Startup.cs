using API.Source.Common.Email;
using API.Source.Common.Helper;
using API.Source.Common.Jwt;

namespace API.Source.Common;

public static class Startup
{
    public static IServiceCollection AddCommonModule(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IEmailClient, EmailClient>()
            .AddScoped<IHelper, Helper.Helper>()
            .AddScoped<IJwtTokenService, JwtTokenService>();

        return serviceCollection;
    }
}