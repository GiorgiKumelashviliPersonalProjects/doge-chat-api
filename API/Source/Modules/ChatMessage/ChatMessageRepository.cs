using API.Source.Config;
using API.Source.Modules.ChatMessage.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Source.Modules.ChatMessage;

public class ChatMessageRepository : IChatMessageRepository
{
    private readonly DataContext _dataContext;

    public ChatMessageRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Model.Entity.ChatMessage> CreateChatMessage(long userId, long receiverUserId, string message)
    {
        var newChatMessage = new Model.Entity.ChatMessage
        {
            SenderId = userId,
            ReceiverId = receiverUserId,
            Message = message
        };

        await _dataContext.ChatMessages.AddAsync(newChatMessage);
        await _dataContext.SaveChangesAsync();

        return newChatMessage;
    }

    public async Task<List<Model.Entity.ChatMessage>> GetMessages(long userId, long receiverId)
    {
        return await _dataContext
            .ChatMessages
            .Where(e => e.SenderId == userId && e.ReceiverId == receiverId)
            .ToListAsync();
    }
}