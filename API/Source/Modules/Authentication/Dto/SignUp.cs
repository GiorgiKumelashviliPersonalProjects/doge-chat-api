using System.ComponentModel.DataAnnotations;
using API.Source.Config;
using API.Source.Model.Enum;

namespace API.Source.Modules.Authentication.Dto;

public class SignUp
{
    public SignUp(
        int id,
        string code,
        string username,
        string firstName,
        string lastName,
        string password,
        string email,
        Gender gender,
        DateTime birthDate
    )
    {
        Id = id;
        Code = code;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        Gender = gender;
        BirthDate = birthDate;
        Email = email;
    }

    public int Id { get; }

    [StringLength(5)] public string Code { get; }

    [RegularExpression(
        Constants.OnlyLettersAndNumbers,
        ErrorMessage = "Username is invalid, can only contain letters or digits"
    )]
    [StringLength(512, MinimumLength = 1)]
    public string Username { get; }

    [StringLength(512, MinimumLength = 1)] public string Email { get; }
    [StringLength(512, MinimumLength = 1)] public string FirstName { get; }
    [StringLength(512, MinimumLength = 1)] public string LastName { get; }
    [StringLength(512, MinimumLength = 6)] public string Password { get; }
    [EnumDataType(typeof(Gender))] public Gender Gender { get; }
    [DataType(DataType.Date)] public DateTime BirthDate { get; }
}