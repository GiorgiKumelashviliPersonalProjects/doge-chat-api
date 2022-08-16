namespace API.Source.Modules.Authentication.Interfaces;

public interface IRequestSignupRepository
{
    Task ClearAllByEmail(string email);
    Task<Model.Entity.RequestSignup> CreateEntity(string email, string code);
    Task<Model.Entity.RequestSignup?> GetById(int id);
    Task DeleteById(long id);

    Task<Model.Entity.RequestSignup?> UpdateById(
        long id,
        string? email = null,
        string? code = null,
        bool? isVerified = null,
        string? uuid = null
    );
}