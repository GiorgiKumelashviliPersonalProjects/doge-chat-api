using API.Source.Exception.Http;
using API.Source.Exception.Validation;
using API.Source.Model.Enum;
using API.Source.Modules.User.Dto;
using API.Source.Modules.User.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Source.Modules.User;

public class UserRepository : IUserRepository
{
    private readonly UserManager<Model.Entity.User> _userManager;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(UserManager<Model.Entity.User> userManager, ILogger<UserRepository> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public Task<bool> CheckUserByEmail(string email)
    {
        return _userManager.Users.AnyAsync(user => user.Email.Equals(email));
    }

    public async Task<List<Model.Entity.User>> GetUsers()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<Model.Entity.User> CreateEntity(
        string firstName,
        string lastName,
        string userName,
        string email,
        string password,
        Gender gender,
        DateTime birthDate
    )
    {
        var user = new Model.Entity.User
        {
            FirstName = firstName,
            LastName = lastName,
            UserName = userName,
            Email = email,
            Gender = gender,
            BirthDate = birthDate,
        };

        var createResult = await _userManager.CreateAsync(user, password);

        if (createResult.Succeeded)
        {
            return user;
        }

        var errors = createResult.Errors.ToList();

        if (errors.Count > 0)
        {
            throw new ValidationException(errors[0].Description);
        }

        // log error
        InternalServerException.ThrowCustomException(_logger, createResult);
        return null!;
    }

    public async Task<Model.Entity.User?> GetUserByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<Model.Entity.User?> GetUserById(
        long userId,
        bool? loadSenderChatMessages = null,
        bool? loadReceiverChatMessages = null
    )
    {
        var query = _userManager.Users.AsNoTracking();

        if (loadSenderChatMessages is true)
        {
            query = query.Include(u => u.SentChatMessages);
        }

        if (loadReceiverChatMessages is true)
        {
            query = query.Include(u => u.ReceivedChatMessages);
        }

        return await query.SingleOrDefaultAsync(u => u.Id == userId);
    }
}