using Supabase;
using TheTranslator.Models;
using TheTranslator.Contracts;

namespace TheTranslator.Services;


public class TextService
{
    private readonly Client _client;

    public TextService(Client client)
    {
        _client = client;
    }

    public async Task<IEnumerable<TextResponse>> GetTextsForUser(int userId)
    {
        var result = await _client
            .From<TextModel>()
            .Where(t => t.UserId == userId)
            .Get();

        return result.Models.Select(t => new TextResponse
        {
            Id = t.Id,
            UserId = t.UserId,
            Title = t.Title,
            Content = t.Content,
            LanguageFrom = t.LanguangeFrom,
            LanguageTo = t.LanguageTo,
        });
    }

    public async Task<int> CreateText(TextModel req)
    {
        var response = await _client.From<TextModel>().Insert(req);
        return response.Models.First().Id;
    }

    public async Task DeleteText(int textId)
    {
        await _client
            .From<TextModel>()
            .Where(t => t.Id == textId)
            .Delete();
    }
}
