using KST.Madiff.CardService.Application.Exceptions;
using KST.Madiff.CardService.Application.UseCases.GetAllowedCardActions;
using Microsoft.AspNetCore.Mvc;

namespace KST.Madiff.CardService.Api.Controllers;
[Route("[controller]")]
[ApiController]
public class CardActionsController(IGetAllowedCardActionsUseCase getAllowedCardActionsUseCase) : ControllerBase
{
    private readonly IGetAllowedCardActionsUseCase _getAllowedCardActionsUseCase = getAllowedCardActionsUseCase;

    [HttpGet("{userId}/{cardNumber}")]
    public async Task<IActionResult> GetAllowedCardActions([FromRoute] string userId, [FromRoute] string cardNumber)
    {
        GetAllowedCardActionsRequest request = new(userId, cardNumber);

        try
        {
            var result = await _getAllowedCardActionsUseCase.ExecuteAsync(request);

            return Ok(result);
        }
        catch (ValidationFailedException ex)
        {
            return ValidationProblem(ex.ModelState);
        }
        catch (CardNotFoundException)
        {
            return NotFound();
        }
    }
}
