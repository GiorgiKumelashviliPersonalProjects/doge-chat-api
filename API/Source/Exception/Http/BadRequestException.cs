using System.Net;

namespace API.Source.Exception.Http;

public class BadRequestException : GenericException
{
  public BadRequestException(string message) : base(message, HttpStatusCode.BadRequest)
  {
  }
}