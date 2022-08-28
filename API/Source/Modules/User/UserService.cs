using API.Source.Model.Enum;
using API.Source.Modules.User.Common;
using API.Source.Modules.User.Interfaces;
using API.Source.Modules.User.RefreshToken;

namespace API.Source.Modules.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public UserService(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<bool> CheckIfEmailExists(string email)
    {
        return await _userRepository.CheckUserByEmail(email);
    }

    public Task<Model.Entity.User?> GetUserById(long userId, GetUserProps? getUserProps = null)
    {
        return _userRepository.GetUserById(userId, getUserProps);
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