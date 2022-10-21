using API.Source.Model.Enum;
using API.Source.Model.Projection;
using API.Source.Modules.User.Dto;

namespace API.Source.Modules.User.Interfaces;

public interface IUserService
{
    Task<bool> CheckIfEmailExists(string email);

    Task<Model.Entity.User?> GetUserById(
        long userId,
        bool? loadSenderChatMessages = null,
        bool? loadReceiverChatMessages = null
    );

    Task<List<UserDto>> GetUsers();

    Task<Model.Entity.User?> GetUserByEmail(string email);

    Task<Model.Entity.User> CreateUser(
        string firstName,
        string lastName,
        string userName,
        string email,
        string password,
        Gender gender,
        DateTime birthDate
    );

    Task AddRefreshTokenByUserId(long userId, string refreshToken);
    Task<UserIdEmailProjection?> GetUserIdByRefreshToken(string refreshToken);
    Task ClearRefreshTokensByUserId(long decodedPayloadUserId);
    Task DeleteRefreshToken(string refreshToken);
    Task<Model.Entity.User?> GetUserByUsername(string bodyUsername);
}