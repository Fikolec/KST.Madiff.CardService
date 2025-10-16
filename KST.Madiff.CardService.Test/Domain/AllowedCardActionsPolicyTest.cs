using KST.Madiff.CardService.Domain.Enums;
using KST.Madiff.CardService.Domain.Policies;
using KST.Madiff.CardService.Domain.ValueObjects;

namespace KST.Madiff.CardService.Test.Domain;

public static class AllowedCardActionsPolicyTest
{
    private static readonly AllowedCardActionsPolicy _policy = new();

    [Theory]
    [ClassData(typeof(AllowedCardActionsPolicyFileData))]
    public static void Should_Return_Correct_Result_For_Action_And_Card_Details(
        CardType cardType,
        CardStatus cardStatus,
        bool isPinSet,
        CardAction cardAction,
        bool expectedResult)
    {
        // Arrange: build a card with given parameters from data file
        var card = new CardDetails("Card1", cardType, cardStatus, isPinSet);

        // Act: get allowed actions based on business rules
        var allowedActions = _policy.GetAllowedCardActions(card);

        // Assert: check if the action is allowed or not
        if (expectedResult)
            Assert.Contains(cardAction, allowedActions);
        else
            Assert.DoesNotContain(cardAction, allowedActions);
    }
}
