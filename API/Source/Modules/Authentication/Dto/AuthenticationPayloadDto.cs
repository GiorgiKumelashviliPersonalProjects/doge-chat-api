namespace API.Source.Modules.Authentication.Dto;

public class AuthenticationPayloadDto
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}