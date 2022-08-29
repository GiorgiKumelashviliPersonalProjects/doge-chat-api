using API.Source.Model.Enum;
using API.Source.Modules.User.Dto;
using API.Source.Modules.User.Interfaces;
using API.Source.Modules.User.RefreshToken;
using AutoMapper;

namespace API.Source.Modules.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IMapper mapper
    )
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _mapper = mapper;
    }

    public async Task<bool> CheckIfEmailExists(string email)
    {
        return await _userRepository.CheckUserByEmail(email);
    }

    public async Task<GetUserDto> GetUserById(long userId,
        bool? loadSenderChatMessages = null,
        bool? loadReceiverChatMessages = null)
    {
        var result = await _userRepository.GetUserById(
            userId: userId,
            loadSenderChatMessages: loadSenderChatMessages,
            loadReceiverChatMessages: loadReceiverChatMessages
        );

        return _mapper.Map<GetUserDto>(result);
    }


    public Task<Model.Entity.User?> GetUserByEmail(string email)
    {
        return _userRepository.GetUserByEmail(email);
    }

    public Task<Model.Entity.User> CreateUser(
        string firstName,
        string lastName,
        string userName,
        string email,
        string password,
        Gender gender,
        DateTime birthDate
    )
    {
        return _userRepository.CreateEntity(firstName, lastName, userName, email, password, gender, birthDate);
    }

    public async Task AddRefreshTokenByUserId(long userId, string refreshToken)
    {
        await _refreshTokenRepository.AddRefreshTokenByUserId(userId, refreshToken);
    }
}