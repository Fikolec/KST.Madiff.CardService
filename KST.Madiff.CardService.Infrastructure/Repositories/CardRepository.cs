using KST.Madiff.CardService.Domain.Interfaces.Repositories;
using KST.Madiff.CardService.Domain.ValueObjects;

namespace KST.Madiff.CardService.Infrastructure.Repositories;
internal class CardRepository(Services.CardService cardService) : ICardRepository
{
    private readonly Services.CardService _cardService = cardService;

    public async Task<CardDetails?> GetCardDetailsAsync(string userId, string cardNumber)
    {
        return await _cardService.GetCardDetails(userId, cardNumber);
    }
}
