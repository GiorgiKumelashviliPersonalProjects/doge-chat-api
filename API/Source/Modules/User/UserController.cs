using API.Source.Extension;
using API.Source.Modules.User.Dto;
using API.Source.Modules.User.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Source.Modules.User;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<GetUserDto?> GetUser()
    {
        var userId = User.GetUserId();
        var user = await _userService.GetUserById(
            userId: userId,
            loadSenderChatMessages: false,
            loadReceiverChatMessages: false
        );

        return _mapper.Map<GetUserDto>(user);
    }
    
    [HttpGet("{id:long}")]
    public async Task<UserDto?> GetUserById([FromRoute] long id)
    {
        var user =  await _userService.GetUserById(
            userId: id,
            loadSenderChatMessages: false,
            loadReceiverChatMessages: false
        );

        return _mapper.Map<UserDto>(user);
    }

    [HttpGet("all")]
    public async Task<List<UserDto>> GetUsers()
    {
        var userId = User.GetUserId();
        var users = await _userService.GetUsers();
        
        // remove current user
        users.RemoveAll(u => u.Id == userId);

        return users;
    }
}