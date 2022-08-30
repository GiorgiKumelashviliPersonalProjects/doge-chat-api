using API.Source.Config;
using API.Source.Modules.Authentication.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Source.Modules.Authentication.RequestSignup;

public class RequestSignupRepository : IRequestSignupRepository
{
    private readonly DataContext _dataContext;

    public RequestSignupRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task ClearAllByEmail(string email)
    {
        var requestSignups = await _dataContext
            .RequestSignups
            .Where(req => req.Email.Equals(email))
            .ToListAsync();

        _dataContext.RequestSignups.RemoveRange(requestSignups);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<Model.Entity.RequestSignup> CreateEntity(string email, string code)
    {
        var requestSignup = new Model.Entity.RequestSignup
        {
            Email = email,
            Code = code,
        };

        _dataContext.RequestSignups.Add(requestSignup);

        await _dataContext.SaveChangesAsync();

        return requestSignup;
    }

    public Task<Model.Entity.RequestSignup?> GetById(int id)
    {
        return _dataContext.RequestSignups.SingleOrDefaultAsync(r => r.Id.Equals(id));
    }

    public async Task DeleteById(long id)
    {
        var result = await _dataContext.RequestSignups.FirstOrDefaultAsync(r => r.Id.Equals(id));

        if (result is not null)
        {
            _dataContext.RequestSignups.Remove(result);

            await _dataContext.SaveChangesAsync();
        }
    }

    public async Task<Model.Entity.RequestSignup?> UpdateById(
        long id,
        string? email = null,
        string? code = null,
        bool? isVerified = null,
        string? uuid = null
    )
    {
        var signUpRequest = await _dataContext.RequestSignups.SingleOrDefaultAsync(request => request.Id == id);

        if (signUpRequest is null)
        {
            return null;
        }

        if (email is not null) signUpRequest.Email = email;
        if (code is not null) signUpRequest.Code = code;

        await _dataContext.SaveChangesAsync();

        return signUpRequest;
    }
}