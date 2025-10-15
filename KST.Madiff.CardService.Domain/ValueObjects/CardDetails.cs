using KST.Madiff.CardService.Domain.Enums;

namespace KST.Madiff.CardService.Domain.ValueObjects;
public record CardDetails(
    string CardNumber,
    CardType CardType,
    CardStatus CardStatus,
    bool IsPinSet
);
