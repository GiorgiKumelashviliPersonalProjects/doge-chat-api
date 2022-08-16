using API.Source.Common.Email;
using API.Source.Common.Helper;
using API.Source.Common.Jwt;
using API.Source.Config;
using API.Source.Exception;
using API.Source.Exception.Http;
using API.Source.Model.Common;
using API.Source.Modules.Authentication.Dto;
using API.Source.Modules.Authentication.Interfaces;
using API.Source.Modules.User.Interfaces;

namespace API.Source.Modules.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IRequestSignupService _requestSignupService;
    private readonly IUserService _userService;
    private readonly IEmailClient _emailClient;
    private readonly IHelper _helper;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthenticationService(
        IRequestSignupService requestSignupService,
        IUserService userService,
        IEmailClient emailClient,
        IHelper helper,
        IJwtTokenService jwtTokenService
    )
    {
        _requestSignupService = requestSignupService;
        _userService = userService;
        _emailClient = emailClient;
        _helper = helper;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<Model.Entity.RequestSignup> SignupRequest(string email)
    {
        var doesEmailExist = await _userService.CheckIfEmailExists(email);
        var randomCode = _helper.GenerateRandomInt();

        if (doesEmailExist)
        {
            throw new ConflictException(ExceptionMessageCode.EmailAlreadyInUse);
        }

        // create signup request in db and send via email
        var result = await _requestSignupService.CreateSignUpRequest(email, randomCode);
        await _emailClient.SendEmailAsync(email, Constants.SignupRequestEmailSubject, randomCode);

        return result;
    }

    public async Task<AuthenticationPayloadDto> SignUpConfirmVerificationCode(SignUpConfirmVerificationCodeDto body)
    {
        var requestSignupById = await _requestSignupService.GetRequestSignupById(body.Id);

        // validate existence
        if (requestSignupById is null)
        {
            throw new NotFoundException(ExceptionMessageCode.SignUpRequestNotFound);
        }

        // validate code correctness
        if (requestSignupById.Code != body.Code)
        {
            throw new ForbiddenException(ExceptionMessageCode.InvalidVerificationCode);
        }

        // then save all data
        var user = await _userService.CreateUser(
            firstName: body.FirstName,
            lastName: body.LastName,
            userName: body.Username,
            email: requestSignupById.Email,
            birthDate: body.BirthDate,
            gender: body.Gender,
            password: body.Password
        );

        var tokenPayload = new AuthenticationTokenPayload(user.Email, user.Id);
        var accessToken = _jwtTokenService.GenerateAccessToken(tokenPayload);
        var refreshToken = _jwtTokenService.GenerateRefreshToken(tokenPayload);

        await _userService.AddRefreshTokenByUserId(user.Id, refreshToken);
        
        // delete request sign up
        await _requestSignupService.DeleteRequestSignUpById(requestSignupById.Id);

        return new AuthenticationPayloadDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}