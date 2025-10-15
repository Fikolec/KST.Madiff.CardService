using KST.Madiff.CardService.Domain.Enums;
using KST.Madiff.CardService.Domain.Policies;
using KST.Madiff.CardService.Domain.ValueObjects;

namespace KST.Madiff.CardService.Test;

public class ActionAllowedPolicyTest
{
    private readonly AllowedCardActionsPolicy _policy = new();

    [Theory]
    [InlineData(CardType.Prepaid, CardStatus.Ordered, true, CardAction.ACTION1, false)]
    [InlineData(CardType.Debit, CardStatus.Inactive, true, CardAction.ACTION5, false)]
    [InlineData(CardType.Credit, CardStatus.Active, true, CardAction.ACTION3, true)]
    [InlineData(CardType.Prepaid, CardStatus.Restricted, true, CardAction.ACTION9, true)]
    [InlineData(CardType.Prepaid, CardStatus.Blocked, true, CardAction.ACTION6, true)]
    [InlineData(CardType.Prepaid, CardStatus.Blocked, false, CardAction.ACTION7, false)]
    [InlineData(CardType.Prepaid, CardStatus.Blocked, true, CardAction.ACTION9, true)]
    [InlineData(CardType.Prepaid, CardStatus.Active, false, CardAction.ACTION6, false)]
    [InlineData(CardType.Prepaid, CardStatus.Active, false, CardAction.ACTION7, true)]
    [InlineData(CardType.Prepaid, CardStatus.Inactive, true, CardAction.ACTION11, true)]
    public void Should_Return_Correct_Result_For_Action_And_Card_Details(
        CardType cardType,
        CardStatus cardStatus,
        bool isPinSet,
        CardAction cardAction,
        bool expectedResult)
    {
        // Arrange
        var card = new CardDetails("Card1", cardType, cardStatus, isPinSet);

        // Act
        var allowedActions = _policy.GetAllowedCardActions(card);

        // Assert
        if (expectedResult)
            Assert.Contains(cardAction, allowedActions);
        else
            Assert.DoesNotContain(cardAction, allowedActions);
    }
}
