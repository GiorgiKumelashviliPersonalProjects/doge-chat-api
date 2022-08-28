using API.Source.Extension;
using API.Source.Modules.User.Common;
using API.Source.Modules.User.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Source.Modules.User;

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<Model.Entity.User?> GetUser()
    {
        var userId = User.GetUserId();
        
        return  await _userService.GetUserById(userId, new GetUserProps
        {
            LoadReceiverChatMessages = true,
            LoadSenderChatMessages = true
        });
    }
}