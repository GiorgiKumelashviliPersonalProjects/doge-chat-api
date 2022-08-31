using API.Source.Modules.Authentication.Dto;
using API.Source.Modules.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Source.Modules.Authentication;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("SignUp/Request")]
    public async Task<OkObjectResult> SignUpRequest([FromBody] SignupRequestDto body)
    {
        var result = await _authenticationService.SignupRequest(body.Email);
        return Ok(new { id = result.Id });
    }

    [HttpPost("SignUp/ConfirmVerificationCode")]
    public async Task<ActionResult<AuthenticationPayloadDto>> SignUpConfirmVerificationCode(
        [FromBody] SignUpConfirmVerificationCodeDto body
    )
    {
        var result = await _authenticationService.SignUpConfirmVerificationCode(body);
        return Ok(result);
    }

    [HttpPost("SignIn")]
    public async Task<ActionResult<AuthenticationPayloadDto>> SignIn([FromBody] SignInDto body)
    {
        var result = await _authenticationService.SignIn(body.Email, body.Password);
        return Ok(result);
    }

    [HttpPost("Refresh")]
    public async Task<ActionResult<AuthenticationPayloadDto>> Refresh([FromBody] RefreshTokenDto body)
    {
        var result = await _authenticationService.Refresh(body.RefreshToken);
        return Ok(result);
    }
}