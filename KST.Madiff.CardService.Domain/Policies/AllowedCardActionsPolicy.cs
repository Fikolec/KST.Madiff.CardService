using KST.Madiff.CardService.Domain.Enums;
using KST.Madiff.CardService.Domain.ValueObjects;

namespace KST.Madiff.CardService.Domain.Policies;
/// <summary>
/// Defines the business policy of allowed actions for a given card.
/// </summary>
public class AllowedCardActionsPolicy
{
    /// <summary>
    /// Gets a list of allowed actions for a given card.
    /// </summary>
    /// <param name="card">The card whose permitted actions will be determined.</param>
    /// <returns>An <see cref="IEnumerable{CardAction}"/> representing allowed actions.</returns>
    public IEnumerable<CardAction> GetAllowedCardActions(CardDetails card)
    {
        List<CardAction> allowedActions = [];

        foreach (CardAction cardAction in Enum.GetValues(typeof(CardAction)))
        {
            if (IsActionAllowed(card, cardAction))
                allowedActions.Add(cardAction);
        }

        return allowedActions;
    }

    /// <summary>
    /// Defines whether the specified <see cref="CardAction"/> is allowed for the specified card.
    /// </summary>
    /// <param name="card">Card for which the action will be checked.</param>
    /// <param name="action">Action to check.</param>
    /// <returns>True if action is allowed, false otherwise.</returns>
    private bool IsActionAllowed(CardDetails card, CardAction action)
    {
        return action switch
        {
            CardAction.ACTION1 => card.CardStatus == CardStatus.Active,
            CardAction.ACTION2 => card.CardStatus == CardStatus.Inactive,
            CardAction.ACTION5 => card.CardType == CardType.Credit,
            CardAction.ACTION6 => IsPinSetAndStatusIn(card, CardStatus.Blocked, CardStatus.Ordered, CardStatus.Inactive, CardStatus.Active),
            CardAction.ACTION7 => card.CardStatus switch
            {
                CardStatus.Blocked => card.IsPinSet,
                CardStatus.Ordered or CardStatus.Inactive or CardStatus.Active => !card.IsPinSet,
                _ => false
            },
            CardAction.ACTION8 => IsStatusNotIn(card, CardStatus.Restricted, CardStatus.Expired, CardStatus.Closed),
            CardAction.ACTION10 or CardAction.ACTION12 or CardAction.ACTION13
                => IsStatusNotIn(card, CardStatus.Restricted, CardStatus.Blocked, CardStatus.Expired, CardStatus.Closed),
            CardAction.ACTION11
                => IsStatusNotIn(card, CardStatus.Ordered, CardStatus.Restricted, CardStatus.Blocked, CardStatus.Expired, CardStatus.Closed),
            _ => true
        };
    }

    private static bool IsStatusIn(CardDetails card, params CardStatus[] statuses)
        => statuses.Contains(card.CardStatus);

    private static bool IsStatusNotIn(CardDetails card, params CardStatus[] statuses)
        => !statuses.Contains(card.CardStatus);

    private static bool IsPinSetAndStatusIn(CardDetails card, params CardStatus[] statuses)
        => card.IsPinSet && IsStatusIn(card, statuses);
}