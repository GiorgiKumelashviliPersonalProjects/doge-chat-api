using System.Net;
using Microsoft.AspNetCore.Identity;

namespace API.Source.Exception.Http;

public class InternalServerException : GenericException
{
    public InternalServerException() : base(
        ExceptionMessageCode.InternalServerException,
        HttpStatusCode.InternalServerError
    )
    {
    }

    public static void ThrowCustomException(ILogger logger, IdentityResult? createResult)
    {
        var arr = createResult?.Errors.Select(e => e.Description) ?? Array.Empty<string>();
        var logParamArgs = string.Join("\n", arr);

        // log first
        logger.LogError("error creating user: {ErrorDescription}", logParamArgs);
        throw new InternalServerException();
    }
}