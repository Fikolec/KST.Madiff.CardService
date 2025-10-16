using KST.Madiff.CardService.Domain.ValueObjects;

namespace KST.Madiff.CardService.Domain.Interfaces.Repositories;
public interface ICardRepository
{
    Task<CardDetails?> GetCardDetailsAsync(string userId, string cardNumber, CancellationToken cancellationToken = default);
}
