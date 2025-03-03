using System.Net;
using Ecommerce.Api.Errors;
using Ecommerce.Application.Exceptions;
using Newtonsoft.Json;

namespace Ecommerce.Api.Middlewares;

public class ExceptionMiddlewares(RequestDelegate next, ILogger<ExceptionMiddlewares> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddlewares> _logger = logger;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.ContentType = "application/json";
            int statusCode;
            var result = string.Empty;
            switch (ex)
            {
                case NotFoundException notFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;
                
                case FluentValidation.ValidationException fluentValidationException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    var errors = fluentValidationException.Errors
                        .Select(error => error.ErrorMessage)
                        .ToArray();
                    var validationJsons = JsonConvert.SerializeObject(errors);
                    result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, errors, validationJsons));
                    break;
                
                case BadRequestException badRequestException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            if (string.IsNullOrEmpty(result))
                result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, [ex.Message], ex.StackTrace));
            
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(result);
        }
    }
}