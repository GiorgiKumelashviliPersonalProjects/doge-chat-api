namespace API.Source.SignalR;

public static class Startup
{
    public static IServiceCollection AddSignalRModule(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<UserConnectionTracker>()
            .AddSignalR();
        
        return serviceCollection;
    }
}