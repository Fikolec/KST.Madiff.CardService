using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KST.Madiff.CardService.Application.Exceptions;
public class ValidationFailedException(ModelStateDictionary modelState)
    : Exception("One or more validation errors occurred.")
{
    public ModelStateDictionary ModelState { get; } = modelState;
}
