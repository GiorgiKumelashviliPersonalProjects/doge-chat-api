using API.Source.Model.Projection;

namespace API.Source.Modules.User.RefreshToken;

public interface IRefreshTokenRepository
{
    Task AddRefreshTokenByUserId(long userId, string refreshToken);
    Task<UserIdEmailProjection?> GetUserIdByRefreshToken(string refreshToken);
    Task DeleteAllByUserId(long userId);
    Task DeleteByValue(string value);
}