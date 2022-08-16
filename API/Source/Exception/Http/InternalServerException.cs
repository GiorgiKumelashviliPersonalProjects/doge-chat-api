using System.Net;

namespace API.Source.Exception.Http;

public class InternalServerException : GenericException
{
  public InternalServerException() : base(
    ExceptionMessageCode.InternalServerException,
    HttpStatusCode.InternalServerError
  )
  {
  }
}