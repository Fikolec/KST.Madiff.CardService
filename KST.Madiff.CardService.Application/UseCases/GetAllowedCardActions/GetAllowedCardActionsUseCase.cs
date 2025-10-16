using KST.Madiff.CardService.Application.Exceptions;
using KST.Madiff.CardService.Application.Validators;
using KST.Madiff.CardService.Domain.Enums;
using KST.Madiff.CardService.Domain.Interfaces.Repositories;
using KST.Madiff.CardService.Domain.Policies;
using Microsoft.Extensions.Logging;

namespace KST.Madiff.CardService.Application.UseCases.GetAllowedCardActions;
/// <inheritdoc cref="IGetAllowedCardActionsUseCase"/>
internal class GetAllowedCardActionsUseCase(
    GetAllowedCardActionsValidator validator,
    ICardRepository cardRepository,
    AllowedCardActionsPolicy policy,
    ILogger<GetAllowedCardActionsUseCase> logger
) : IGetAllowedCardActionsUseCase
{
    private readonly GetAllowedCardActionsValidator _validator = validator;
    private readonly ICardRepository _cardRepository = cardRepository;
    private readonly AllowedCardActionsPolicy _policy = policy;
    private readonly ILogger<GetAllowedCardActionsUseCase> _logger = logger;

    public async Task<GetAllowedCardActionsResponse> ExecuteAsync(GetAllowedCardActionsRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Executing GetAllowedCardActionsUseCase for UserId={UserId}, CardNumber={CardNumber}.",
            request.UserId,
            request.CardNumber);

        _validator.Validate(request);

        var card = await _cardRepository.GetCardDetailsAsync(request.UserId, request.CardNumber, cancellationToken)
            ?? throw new CardNotFoundException(request.UserId, request.CardNumber);

        IEnumerable<CardAction> allowedActions = _policy.GetAllowedCardActions(card);
        IEnumerable<string> mappedActions = allowedActions.Select(a => a.ToString());
        GetAllowedCardActionsResponse response = new(mappedActions);

        _logger.LogInformation(
            "Allowed actions for CardNumber={CardNumber}: {Actions}.",
            request.CardNumber,
            string.Join(", ", mappedActions));

        return response;
    }
}
