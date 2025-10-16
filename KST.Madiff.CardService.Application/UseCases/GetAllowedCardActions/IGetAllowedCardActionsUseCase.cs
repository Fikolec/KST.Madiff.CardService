using KST.Madiff.CardService.Application.Interfaces;

namespace KST.Madiff.CardService.Application.UseCases.GetAllowedCardActions;
/// <summary>
/// Handles the process of determining the actions that are allowed for a specific card based on the provided request.
/// </summary>
public interface IGetAllowedCardActionsUseCase : IUseCase<GetAllowedCardActionsRequest, GetAllowedCardActionsResponse>
{
}
