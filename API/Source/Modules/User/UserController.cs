using API.Source.Extension;
using API.Source.Modules.User.Dto;
using API.Source.Modules.User.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Source.Modules.User;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<GetUserDto> GetUser()
    {
        var userId = User.GetUserId();

        return await _userService.GetUserById(
            userId: userId,
            loadSenderChatMessages: true,
            loadReceiverChatMessages: true
        );
    }

    [HttpGet("all")]
    public Task<List<UserDto>> GetUsers()
    {
        return _userService.GetUsers();
    }
}