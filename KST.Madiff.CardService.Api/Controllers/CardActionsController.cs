using KST.Madiff.CardService.Application.Exceptions;
using KST.Madiff.CardService.Application.UseCases.GetAllowedCardActions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KST.Madiff.CardService.Api.Controllers;
[Route("[controller]")]
[ApiController]
[Produces("application/json")]
public class CardActionsController(
    ILogger<CardActionsController> logger,
    IGetAllowedCardActionsUseCase getAllowedCardActionsUseCase) : ControllerBase
{
    private readonly ILogger<CardActionsController> _logger = logger;
    private readonly IGetAllowedCardActionsUseCase _getAllowedCardActionsUseCase = getAllowedCardActionsUseCase;

    /// <summary>
    /// Retrieving allowed actions for a specific card.
    /// </summary>
    /// <param name="userId">Id of the user assigned to a given card.</param>
    /// <param name="cardNumber">Card number of the card whose actions you want to retrieve.</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="IActionResult"/> containing <see cref="GetAllowedCardActionsResponse"/> if request is successfull, otherwise <see cref="ValidationProblemDetails"/> or <see cref="ProblemDetails"/>.</returns>
    [HttpGet("{userId}/{cardNumber}")]
    [ProducesResponseType<GetAllowedCardActionsResponse>((int)HttpStatusCode.OK)]
    [ProducesResponseType<ValidationProblemDetails>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAllowedCardActions(
        [FromRoute] string userId,
        [FromRoute] string cardNumber,
        CancellationToken cancellationToken)
    {
        // TODO: Add middleware to translate domain and validation exceptions
        // into proper IActionResult responses (e.g. 400, 404, 500).

        GetAllowedCardActionsRequest request = new(userId, cardNumber);

        try
        {
            var result = await _getAllowedCardActionsUseCase.ExecuteAsync(request, cancellationToken);

            return Ok(result);
        }
        catch (ValidationFailedException ex)
        {
            _logger.LogWarning(
                "GetAllowedCardActions: Validation failed. UserId={UserId}, CardNumber={CardNumber}. Errors={Errors}",
                userId,
                cardNumber,
                string.Join(", ", ex.ModelState.Keys));

            return ValidationProblem(ex.ModelState);
        }
        catch (CardNotFoundException)
        {
            _logger.LogWarning(
                "GetAllowedCardActions: Card not found. UserId={UserId}, CardNumber={CardNumber}.",
                userId,
                cardNumber);

            return NotFound();
        }
    }
}
