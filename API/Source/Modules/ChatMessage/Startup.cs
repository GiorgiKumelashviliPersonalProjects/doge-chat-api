using API.Source.Modules.ChatMessage.Interfaces;

namespace API.Source.Modules.ChatMessage;

public static class Startup
{
    public static IServiceCollection AddChatMessageModule(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IChatMessageRepository, ChatMessageRepository>()
            .AddScoped<IChatMessageService, ChatMessageService>();

        return serviceCollection;
    }
}