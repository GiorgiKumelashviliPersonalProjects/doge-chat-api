using API.Source.Exception.Http;
using API.Source.Exception.Validation;
using API.Source.Modules.Chat.Dto;
using API.Source.Modules.ChatMessage.Interfaces;
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

    public async Task SaveMessage(long userId, SendChatMessageDto sendChatMessageDto)
    {
        // get specified user
        var receiverUser = await _userService.GetUserById(sendChatMessageDto.UserId);

        if (sendChatMessageDto.UserId == userId)
        {
            throw new ValidationException("You are sending to yourself bruh");
        }

        if (receiverUser is null)
        {
            throw new NotFoundException("Specified user not found");
        }

        var newChatMessage = await _chatMessageRepository.CreateChatMessage(
            userId: userId,
            receiverUserId:receiverUser.Id,
            message:sendChatMessageDto.Message
        );
    }
}