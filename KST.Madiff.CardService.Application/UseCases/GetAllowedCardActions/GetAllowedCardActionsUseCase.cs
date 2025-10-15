using KST.Madiff.CardService.Application.Exceptions;
using KST.Madiff.CardService.Application.Validators;
using KST.Madiff.CardService.Domain.Enums;
using KST.Madiff.CardService.Domain.Interfaces.Repositories;
using KST.Madiff.CardService.Domain.Policies;

namespace KST.Madiff.CardService.Application.UseCases.GetAllowedCardActions;
internal class GetAllowedCardActionsUseCase(
    GetAllowedCardActionsValidator validator,
    ICardRepository cardRepository,
    AllowedCardActionsPolicy policy
) : IGetAllowedCardActionsUseCase
{
    private readonly GetAllowedCardActionsValidator _validator = validator;
    private readonly ICardRepository _cardRepository = cardRepository;
    private readonly AllowedCardActionsPolicy _policy = policy;

    public async Task<GetAllowedCardActionsResponse> ExecuteAsync(GetAllowedCardActionsRequest request)
    {
        _validator.Validate(request);

        var card = await _cardRepository.GetCardDetailsAsync(request.UserId, request.CardNumber)
            ?? throw new CardNotFoundException(request.UserId, request.CardNumber);

        IEnumerable<CardAction> allowedActions = _policy.GetAllowedCardActions(card);
        IEnumerable<string> mappedActions = allowedActions.Select(a => a.ToString());
        GetAllowedCardActionsResponse response = new(mappedActions);

        return response;
    }
}
