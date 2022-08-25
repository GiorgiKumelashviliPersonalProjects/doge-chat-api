using API.Source.Exception;
using API.Source.Exception.Http;
using API.Source.Model.Dto.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.Source.Middleware;

public class ExceptionMiddleware: IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (System.Exception e)
        {
            if (e is not GenericException && e.InnerException is not null)
            {
                while (e.InnerException is not  null)
                {
                    e = e.InnerException;
                }
            }

            const string message = "_____________________{Message}_____________________\n{Trace}";
            var response = context.Response;
            
            _logger.LogInformation(message, e.Message, e.StackTrace);
            
            if (!response.HasStarted)
            {
                var resolvedException = e as GenericException ?? new InternalServerException();
                var responseDto = new GenericExceptionDto
                {
                    Message = resolvedException.Message,
                    StatusCode = resolvedException.StatusCode
                };

                response.ContentType = "application/json";
                response.StatusCode = (int)resolvedException.StatusCode;

                await response.WriteAsync(
                    JsonConvert.SerializeObject(
                        responseDto,
                        new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }
                    )
                );
            }
            else { _logger.LogWarning("Can't write error response. Response has already started"); }
        }
    }
}