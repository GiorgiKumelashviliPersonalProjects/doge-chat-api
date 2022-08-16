using System.Text;
using API.Source.Modules.Authentication.Interfaces;
using API.Source.Modules.Authentication.RequestSignup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Source.Modules.Authentication;

public static class Startup
{
    public static IServiceCollection AddAuthenticationModule(
        this IServiceCollection serviceCollection,
        IConfiguration configuration
    )
    {
        serviceCollection
            .AddScoped<IAuthenticationService, AuthenticationService>()
            .AddScoped<IRequestSignupService, RequestSignupService>()
            .AddScoped<IRequestSignupRepository, RequestSignupRepository>()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Authentication:AccessTokenSecret"])
                    )
                };
            });

        return serviceCollection;
    }
}