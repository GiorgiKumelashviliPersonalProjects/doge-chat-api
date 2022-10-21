using API.Source.Exception.Http;
using API.Source.Modules.ChatMessage.Dto;
using API.Source.Modules.ChatMessage.Interfaces;
using API.Source.Modules.User.Dto;
using API.Source.Modules.User.Interfaces;
using AutoMapper;

namespace API.Source.Modules.ChatMessage;

public class ChatMessageService : IChatMessageService
{
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public ChatMessageService(IChatMessageRepository chatMessageRepository, IUserService userService, IMapper mapper)
    {
        _chatMessageRepository = chatMessageRepository;
        _userService = userService;
        _mapper = mapper;
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
        var receiverUserDto = _mapper.Map<GetUserDto>(receiverUser);

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
            ReceivedUser = receiverUserDto
        };
    }

    public Task<List<Model.Entity.ChatMessage>> GetMessages(long userId, long receiverId)
    {
        return _chatMessageRepository.GetMessages(userId, receiverId);
    }
}