namespace API.Source.Modules.Authentication.Interfaces;

public interface IRequestSignupRepository
{
    Task ClearAllByEmail(string email);
    Task<Model.Entity.RequestSignup> CreateEntity(string email, string code);
    Task<Model.Entity.RequestSignup?> GetByIdAndEmail(int id, string email);
    Task DeleteById(long id);
}