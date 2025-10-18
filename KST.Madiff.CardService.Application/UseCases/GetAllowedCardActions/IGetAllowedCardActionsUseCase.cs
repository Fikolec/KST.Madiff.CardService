using KST.Madiff.CardService.Application.Interfaces;

namespace KST.Madiff.CardService.Application.UseCases.GetAllowedCardActions;
/// <summary>
/// Covers the process of determining allowed actions for a given card.
/// </summary>
public interface IGetAllowedCardActionsUseCase : IUseCase<GetAllowedCardActionsRequest, GetAllowedCardActionsResponse>
{
}
