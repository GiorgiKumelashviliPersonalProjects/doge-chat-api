using System.ComponentModel.DataAnnotations;

namespace API.Source.Modules.Authentication.Dto;

public class SignupConfirmDto
{
    [EmailAddress, StringLength(512)] public string Email { get; set; }
}