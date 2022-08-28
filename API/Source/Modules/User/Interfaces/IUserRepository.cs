using API.Source.Model.Enum;
using API.Source.Modules.User.Common;

namespace API.Source.Modules.User.Interfaces;

public interface IUserRepository
{
    Task<bool> CheckUserByEmail(string email);

    Task<Model.Entity.User?> GetUserByEmail(string email);

    Task<Model.Entity.User?> GetUserById(long userId, GetUserProps? getUserProps = null);

    Task<Model.Entity.User> CreateEntity(string firstName,
        string lastName,
        string userName,
        string email,
        string password,
        Gender gender,
        DateTime birthDate
    );
}