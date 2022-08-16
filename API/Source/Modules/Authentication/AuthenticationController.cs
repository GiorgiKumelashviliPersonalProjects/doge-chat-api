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
    public async Task<OkObjectResult> SignUpConfirmVerificationCode(
        [FromBody] SignUpConfirmVerificationCodeDto body
    )
    {
        var result = await _authenticationService.SignUpConfirmVerificationCode(body);
        return Ok(result);
    }
}