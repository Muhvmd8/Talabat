global using System.Net;
namespace Shared.ErrorsModels;
public class ValidationErrorResponse
{
    public int StatusCode { get; set; } = (int)HttpStatusCode.BadRequest;
    public string Message { get; set; } = "Validation Failed!";
    public IEnumerable<ValidationError> ValidationErrors { get; set; } = [];
}
