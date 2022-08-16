namespace API.Source.Config;

public class Constants
{
    public const string UuidRegex = @"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$";
    public const string onlyLettersAndNumbers = @"^[a-zA-Z0-9]+$";
    
    public const string SignupRequestEmailSubject = "Sign up";
}