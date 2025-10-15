namespace KST.Madiff.CardService.Application.Exceptions;
public class CardNotFoundException(string userId, string cardNumber)
    : Exception($"Card {cardNumber} not found for user id {userId}.")
{
}
