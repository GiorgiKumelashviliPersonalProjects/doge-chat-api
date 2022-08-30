using System.Net;

namespace API.Source.Exception.Http;

public class ForbiddenException : GenericException
{
    public ForbiddenException(string message) : base(message, HttpStatusCode.Forbidden)
    {
    }
}