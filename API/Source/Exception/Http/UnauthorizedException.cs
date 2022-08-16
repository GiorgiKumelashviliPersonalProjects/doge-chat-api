using System.Net;

namespace API.Source.Exception.Http;

public class UnauthorizedException : GenericException
{
  public UnauthorizedException(string message) : base(message, HttpStatusCode.Unauthorized)
  {
  }
}