using KST.Madiff.CardService.Application.Exceptions;
using KST.Madiff.CardService.Application.UseCases.GetAllowedCardActions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KST.Madiff.CardService.Application.Validators;
internal class GetAllowedCardActionsValidator
{
    public void Validate(GetAllowedCardActionsRequest request)
    {
        ModelStateDictionary modelState = new();

        if (string.IsNullOrWhiteSpace(request.UserId) || !request.UserId.StartsWith("User"))
            modelState.AddModelError(nameof(request.UserId), "UserId has invalid format.");

        if (string.IsNullOrWhiteSpace(request.CardNumber) || !request.CardNumber.StartsWith("Card"))
            modelState.AddModelError(nameof(request.CardNumber), "CardNumber has invalid format.");

        if (!modelState.IsValid)
            throw new ValidationFailedException(modelState);
    }
}
