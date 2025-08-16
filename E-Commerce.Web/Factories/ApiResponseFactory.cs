namespace E_Commerce.Web.Factories;
public static class ApiResponseFactory
{
    public static IActionResult GenerateApiValidationErrorsResponse(ActionContext context)
    {
        var errors = context.ModelState.Where(m => m.Value.Errors.Any())
                    .Select(m => new ValidationError
                    {
                        Field = m.Key,
                        Errors = m.Value.Errors.Select(e => e.ErrorMessage)
                    });
        var response = new ValidationErrorResponse
        {
            ValidationErrors = errors
        };
        return new BadRequestObjectResult(response);
    }
}
