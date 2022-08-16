using System.Net;

namespace API.Source.Model.Dto.Common;

public class GenericExceptionDto
{
    public string Message { get; set; }

    public HttpStatusCode StatusCode { get; set; }
}