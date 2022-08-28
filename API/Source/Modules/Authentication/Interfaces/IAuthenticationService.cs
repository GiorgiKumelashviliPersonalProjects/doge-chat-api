using API.Source.Modules.Authentication.Dto;

namespace API.Source.Modules.Authentication.Interfaces;

public interface IAuthenticationService
{
    Task<Model.Entity.RequestSignup> SignupRequest(string email);
    Task<AuthenticationPayloadDto> SignUpConfirmVerificationCode(SignUpConfirmVerificationCodeDto body);
    Task<AuthenticationPayloadDto> SignIn(string email, string password);
}