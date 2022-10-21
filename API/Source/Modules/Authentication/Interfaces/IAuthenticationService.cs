using API.Source.Modules.Authentication.Dto;

namespace API.Source.Modules.Authentication.Interfaces;

public interface IAuthenticationService
{
    Task<Model.Entity.RequestSignup> SignUpConfirm(string email);
    Task<AuthenticationPayloadDto> SignUp(SignUp body);
    
    Task<AuthenticationPayloadDto> SignIn(string email, string password);
    Task<AuthenticationPayloadDto> Refresh(string bodyRefreshToken);
}