using System.Net;

namespace API.Source.Exception.Validation;

public class ValidationException : GenericException
{
    public ValidationException(string message) : base(message, HttpStatusCode.BadRequest)
    {
    }
}