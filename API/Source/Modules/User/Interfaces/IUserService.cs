using API.Source.Model.Enum;

namespace API.Source.Modules.User.Interfaces;

public interface IUserService
{
    Task<bool> CheckIfEmailExists(string email);

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
}