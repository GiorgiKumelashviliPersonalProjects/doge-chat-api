using API.Source.Common;
using API.Source.Config;
using API.Source.Middleware;
using API.Source.Model;
using API.Source.Modules.Authentication;
using API.Source.Modules.ChatMessage;
using API.Source.Modules.User;
using API.Source.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerUI;

//================================================
//TODO permission and roles
//TODO date time problem needs solving
//TODO add online status updates via socket connect and disconnect
//================================================

// for date in postgres
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Postgresql");
var authorizationPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

// Add services to the container.
builder.Services
    .AddCors()
    .AddSwaggerModule()
    .AddScoped<ExceptionMiddleware>()
    .AddDbContext<DataContext>(o => o.UseNpgsql(connectionString))
    .AddSecurityModule()
    .AddCommonModule()
    .AddChatMessageModule()
    .AddAuthenticationModule(builder.Configuration)
    .AddUserModule()
    .AddAutoMapper(typeof(AutoMapperProfileConfig).Assembly)
    .AddEndpointsApiExplorer()
    .AddControllers(o => o.Filters.Add(new AuthorizeFilter(authorizationPolicy)))
    .AddNewtonsoftJson(o =>
    {
        // for double relation loop removal
        o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

// builder.Services.AddAutoMapper();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocExpansion(DocExpansion.None);
        options.EnablePersistAuthorization();
    });
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(policyBuilder =>
{
    policyBuilder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed(_ => true)
        .AllowCredentials();
});

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();