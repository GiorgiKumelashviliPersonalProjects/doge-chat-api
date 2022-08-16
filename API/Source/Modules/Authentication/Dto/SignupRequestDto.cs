using System.ComponentModel.DataAnnotations;

namespace API.Source.Modules.Authentication.Dto;

public class SignupRequestDto
{
    [EmailAddress, StringLength(512)]
    public string Email { get; set; }
}