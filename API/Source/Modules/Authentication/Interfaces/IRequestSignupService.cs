namespace API.Source.Modules.Authentication.Interfaces;

public interface IRequestSignupService
{
    Task<Model.Entity.RequestSignup> CreateSignUpRequest(string email, string code);
    Task<Model.Entity.RequestSignup?> GetRequestSignupByIdAndEmail(int id, string email);
    Task DeleteRequestSignUpById(long id);
}