using System.Net;

namespace API.Source.Exception.Http;

public class ConflictException : GenericException
{
    public ConflictException(string message) : base(message, HttpStatusCode.Conflict)
    {
    }
}