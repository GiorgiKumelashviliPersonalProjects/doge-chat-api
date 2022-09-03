namespace API.Source.SignalR;

public static class Startup
{
    public static IServiceCollection AddSignalRModule(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IMainHubService, MainHubService>()
            .AddSingleton<UserConnectionTracker>()
            .AddSignalR();
        
        return serviceCollection;
    }
}