using System.Net;

namespace API.Source.Exception;

public class GenericException : System.Exception
{
    public HttpStatusCode StatusCode { get; }

    protected GenericException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}