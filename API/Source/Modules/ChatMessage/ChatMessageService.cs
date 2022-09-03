using API.Source.Exception.Http;
using API.Source.Exception.Validation;
using API.Source.Modules.ChatMessage.Dto;
using API.Source.Modules.ChatMessage.Interfaces;
using API.Source.Modules.User.Dto;
using API.Source.Modules.User.Interfaces;

namespace API.Source.Modules.ChatMessage;

public class ChatMessageService : IChatMessageService
{
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IUserService _userService;

    public ChatMessageService(IChatMessageRepository chatMessageRepository, IUserService userService)
    {
        _chatMessageRepository = chatMessageRepository;
        _userService = userService;
    }
    
    public class SaveMessageResponse
    {
        public Model.Entity.ChatMessage ChatMessage { get; set; }
        public GetUserDto ReceivedUser { get; set; }
    }

    public async Task<SaveMessageResponse> SaveMessage(long userId, SendChatMessageDto sendChatMessageDto)
    {
        // get specified user
        var receiverUser = await _userService.GetUserById(sendChatMessageDto.UserId);

        if (sendChatMessageDto.UserId == userId)
        {
            throw new BadRequestException("You are sending to yourself bruh");
        }

        if (receiverUser is null)
        {
            throw new NotFoundException("Specified user not found");
        }

        var chatMessage = await _chatMessageRepository.CreateChatMessage(
            userId: userId,
            receiverUserId: receiverUser.Id,
            message: sendChatMessageDto.Message
        );

        return new()
        {
            ChatMessage = chatMessage,
            ReceivedUser = receiverUser
        };
    }

    public Task<List<Model.Entity.ChatMessage>> GetMessages(long userId, long receiverId)
    {
        return _chatMessageRepository.GetMessages(userId, receiverId);
    }
}