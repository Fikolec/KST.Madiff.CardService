using KST.Madiff.CardService.Domain.Enums;
using System.Collections;
using System.Text.Json;

namespace KST.Madiff.CardService.Test;
internal class AllowedCardActionsPolicyFileData : IEnumerable<object[]>
{
    private readonly List<object[]> _data;

    public AllowedCardActionsPolicyFileData()
    {
        var filePath = Path.Combine(AppContext.BaseDirectory, "TestData", "AllowedCardActionsPolicyTestData.json");
        var json = File.ReadAllText(filePath);
        var items = JsonSerializer.Deserialize<List<CardActionRecord>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip
        })!;

        _data = [.. items.Select(i => new object[]
        {
            Enum.Parse<CardType>(i.CardType),
            Enum.Parse<CardStatus>(i.CardStatus),
            i.IsPinSet,
            Enum.Parse<CardAction>(i.CardAction),
            i.ExpectedResult
        })];
    }

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public record CardActionRecord(string CardType, string CardStatus, bool IsPinSet, string CardAction, bool ExpectedResult);