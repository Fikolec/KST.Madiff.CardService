using KST.Madiff.CardService.Application.UseCases.GetAllowedCardActions;

namespace KST.Madiff.CardService.Application.Interfaces;
internal interface IValidator<in TValidated>
{
    public void Validate(GetAllowedCardActionsRequest request);
}
