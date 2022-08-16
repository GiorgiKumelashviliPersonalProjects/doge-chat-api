namespace API.Source.Common.Helper;

public class Helper : IHelper
{
    public string GenerateRandomInt()
    {
        var random = new Random();
        var randomCode = random.Next(10000, 99999);

        return randomCode.ToString();
    }
}