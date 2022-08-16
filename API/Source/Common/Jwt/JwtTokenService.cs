using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Source.Exception.Http;
using API.Source.Model.Common;
using Microsoft.IdentityModel.Tokens;

namespace API.Source.Common.Jwt;

[SuppressMessage("ReSharper", "PrivateFieldCanBeConvertedToLocalVariable")]
public class JwtTokenService : IJwtTokenService
{
    private readonly ILogger<JwtTokenService> _logger;

    // access token
    private readonly SigningCredentials _accessTokenSigningCredentials;
    private readonly SecurityKey _accessTokenKey;
    private readonly int _accessTokenExpirationInMinutes;

    // refresh token
    private readonly SigningCredentials _refreshTokenSigningCredentials;
    private readonly SecurityKey _refreshTokenKey;
    private readonly int _refreshTokenExpirationInMinutes;

    public JwtTokenService(IConfiguration configuration, ILogger<JwtTokenService> logger)
    {
        _logger = logger;

        // get envs
        var accessTokenSecret = configuration["Authentication:AccessTokenSecret"];
        var accessTokenExpMin = configuration["Authentication:AccessTokenExpirationInMinutes"];
        var refreshTokenSecret = configuration["Authentication:RefreshTokenSecret"];
        var refreshTokenExpMin = configuration["Authentication:RefreshTokenExpirationInMinutes"];

        const string alg = SecurityAlgorithms.HmacSha512Signature;

        _accessTokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessTokenSecret));
        _accessTokenSigningCredentials = new SigningCredentials(_accessTokenKey, alg);

        _refreshTokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(refreshTokenSecret));
        _refreshTokenSigningCredentials = new SigningCredentials(_refreshTokenKey, alg);

        var didParseAccessTokenExpiration = int.TryParse(accessTokenExpMin, out _accessTokenExpirationInMinutes);
        var didParseRefreshTokenExpiration = int.TryParse(refreshTokenExpMin, out _refreshTokenExpirationInMinutes);

        if (!didParseRefreshTokenExpiration || !didParseAccessTokenExpiration)
        {
            throw new InternalServerException();
        }
    }

    private static string GenerateJwtToken
    (
        AuthenticationTokenPayload payload,
        int expirationInMinutes,
        SigningCredentials signingCredentials
    )
    {
        var claims = new List<Claim>
        {
            new(AppClaimType.Email, payload.Email),
            new(AppClaimType.UserId, payload.UserId.ToString()),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(expirationInMinutes),
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    //|========================================
    //| METHODS
    //|========================================

    public AuthenticationTokenPayload? DecodeToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);

        var emailClaim = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == AppClaimType.Email);
        var userIdClaim = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == AppClaimType.UserId);

        if (emailClaim is null || userIdClaim is null)
        {
            return null;
        }

        return !long.TryParse(userIdClaim.Value, out var userId)
            ? null
            : new AuthenticationTokenPayload(emailClaim.Value, userId);
    }

    public bool ValidateRefreshToken(string token)
    {
        var validationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = _refreshTokenKey
        };

        try
        {
            new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out _);
            return true;
        }
        catch (System.Exception e)
        {
            _logger.LogError("Error validating token, {Message}, {StackTrace}", e.Message, e.StackTrace);
        }

        return false;
    }

    public string GenerateAccessToken(AuthenticationTokenPayload payload)
    {
        return GenerateJwtToken(
            payload,
            _accessTokenExpirationInMinutes,
            _accessTokenSigningCredentials
        );
    }

    public string GenerateRefreshToken(AuthenticationTokenPayload payload)
    {
        return GenerateJwtToken(
            payload,
            _refreshTokenExpirationInMinutes,
            _refreshTokenSigningCredentials
        );
    }
}