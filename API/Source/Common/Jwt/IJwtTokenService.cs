using API.Source.Model.Common;

namespace API.Source.Common.Jwt;

public interface IJwtTokenService
{
    public AuthenticationTokenPayload? DecodeToken(string token);
    public bool ValidateRefreshToken(string token);
    public string GenerateAccessToken(AuthenticationTokenPayload payload);
    public string GenerateRefreshToken(AuthenticationTokenPayload payload);
}