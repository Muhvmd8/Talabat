namespace Shared.ErrorsModels;
public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; } = default!;
    public List<string>? Errors { get; set; }
}
