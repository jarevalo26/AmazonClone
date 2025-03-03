using Newtonsoft.Json;

namespace Ecommerce.Api.Errors;

public class CodeErrorResponse
{
    [JsonProperty(PropertyName = "statusCode")]
    public int StatusCode { get; set; }
    
    [JsonProperty(PropertyName = "message")]
    public string[]? Message { get; set; }

    public CodeErrorResponse(int statusCode, string[]? message = null)
    {
        StatusCode = statusCode;
        if (message is null)
        {
            Message = [];
            var text = GetDefaultMessageStatusCode(statusCode);
            Message[0] = text;
        }
        else
        {
            Message = message;
        }
    }

    private static string GetDefaultMessageStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "the request sent has errors",
            401 => "You do not have authorization for this resource",
            404 => "The requested resource was not found",
            500 => "errors occurred on the server",
            _ => string.Empty
        };
    }
}