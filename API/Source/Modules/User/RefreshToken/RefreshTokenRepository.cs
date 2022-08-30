using API.Source.Model;
using API.Source.Model.Projection;
using Microsoft.EntityFrameworkCore;

namespace API.Source.Modules.User.RefreshToken;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly DataContext _dataContext;

    public RefreshTokenRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task AddRefreshTokenByUserId(long userId, string refreshToken)
    {
        await _dataContext.RefreshTokens.AddAsync(
            new Model.Entity.RefreshToken
            {
                Value = refreshToken,
                UserId = userId,
            }
        );

        await _dataContext.SaveChangesAsync();
    }

    public async Task<UserIdEmailProjection?> GetUserIdByRefreshToken(string refreshToken)
    {
        return await _dataContext.RefreshTokens
            .Where(rt => rt.Value == refreshToken)
            .Include(rt => rt.User)
            .Select(rt => new UserIdEmailProjection { UserId = rt.UserId, Email = rt.User.Email })
            .FirstOrDefaultAsync();
    }

    public async Task DeleteAllByUserId(long userId)
    {
        var refreshTokens = await _dataContext
            .RefreshTokens
            .Where(token => token.UserId == userId)
            .ToListAsync();

        _dataContext.RefreshTokens.RemoveRange(refreshTokens);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteByValue(string value)
    {
        var entity = await _dataContext
            .RefreshTokens
            .Where(rt => rt.Value == value)
            .SingleOrDefaultAsync();
        
        if (entity is null)
        {
            return;
        }

        _dataContext.RefreshTokens.Remove(entity);
        await _dataContext.SaveChangesAsync();
    }
}