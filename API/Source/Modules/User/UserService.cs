using API.Source.Model.Enum;
using API.Source.Model.Projection;
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

    public async Task<Model.Entity.User?> GetUserById(long userId,
        bool? loadSenderChatMessages = null,
        bool? loadReceiverChatMessages = null)
    {
        return await _userRepository.GetUserById(
            userId: userId,
            loadSenderChatMessages: loadSenderChatMessages,
            loadReceiverChatMessages: loadReceiverChatMessages
        );
    }

    public async Task<List<UserDto>> GetUsers()
    {
        var users = await _userRepository.GetUsers();
        return _mapper.Map<List<UserDto>>(users);
    }


    public async Task<Model.Entity.User?> GetUserByEmail(string email)
    {
        return await _userRepository.GetUserByEmail(email);
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

    public async Task<UserIdEmailProjection?> GetUserIdByRefreshToken(string refreshToken)
    {
        return await _refreshTokenRepository.GetUserIdByRefreshToken(refreshToken);
    }

    public async Task ClearRefreshTokensByUserId(long decodedPayloadUserId)
    {
        await _refreshTokenRepository.DeleteAllByUserId(decodedPayloadUserId);
    }

    public async Task DeleteRefreshToken(string refreshToken)
    {
        await _refreshTokenRepository.DeleteByValue(refreshToken);
    }

    public async Task<Model.Entity.User?> GetUserByUsername(string username)
    {
        return await _userRepository.GetUserByUsername(username);
    }
}