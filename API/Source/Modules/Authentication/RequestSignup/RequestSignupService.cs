using API.Source.Modules.Authentication.Interfaces;

namespace API.Source.Modules.Authentication.RequestSignup;

public class RequestSignupService : IRequestSignupService
{
    private readonly IRequestSignupRepository _requestSignupRepository;

    public RequestSignupService(IRequestSignupRepository requestSignupRepository)
    {
        _requestSignupRepository = requestSignupRepository;
    }

    public async Task<Model.Entity.RequestSignup> CreateSignUpRequest(string email, string code)
    {
        // clear all previous sign up request
        await _requestSignupRepository.ClearAllByEmail(email);

        // create new one
        return await _requestSignupRepository.CreateEntity(email, code);
    }

    public Task<Model.Entity.RequestSignup?> GetRequestSignupByIdAndEmail(int id, string email)
    {
        return _requestSignupRepository.GetByIdAndEmail(id, email);
    }

    public Task DeleteRequestSignUpById(long id)
    {
        return _requestSignupRepository.DeleteById(id);
    }
}