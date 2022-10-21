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
                // Token Validation Configuration 
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
                
                // For SignalR
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        
                        Console.WriteLine(!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"));

                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        return serviceCollection;
    }
}