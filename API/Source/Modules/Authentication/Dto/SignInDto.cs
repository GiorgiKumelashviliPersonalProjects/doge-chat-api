using System.ComponentModel.DataAnnotations;

namespace API.Source.Modules.Authentication.Dto;

public class SignInDto
{
    [EmailAddress, StringLength(512)] public string Email { get; set; } = null!;

    [StringLength(512, MinimumLength = 6)] public string Password { get; set; } = null!;
}