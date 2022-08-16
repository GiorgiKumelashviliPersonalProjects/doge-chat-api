using API.Source.Common;
using API.Source.Config;
using API.Source.Middleware;
using API.Source.Model;
using API.Source.Modules.Authentication;
using API.Source.Modules.User;
using API.Source.Security;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Postgresql");

// for date in postgres
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add services to the container.
builder.Services
    .AddCors()
    .AddSwaggerModule()
    .AddScoped<ExceptionMiddleware>()
    .AddDbContext<DataContext>(o => o.UseNpgsql(connectionString))
    .AddSecurityModule()
    .AddCommonModule()
    .AddAuthenticationModule(builder.Configuration)
    .AddUserModule()
    .AddEndpointsApiExplorer()
    .AddControllers();

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

// app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


//TODO date time problem needs solving