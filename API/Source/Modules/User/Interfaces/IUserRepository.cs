using API.Source.Model.Enum;

namespace API.Source.Modules.User.Interfaces;

public interface IUserRepository
{
    Task<bool> GetUserByEmail(string email);

    Task<Model.Entity.User> CreateEntity(string firstName,
        string lastName,
        string userName,
        string email,
        string password,
        Gender gender,
        DateTime birthDate
    );
}