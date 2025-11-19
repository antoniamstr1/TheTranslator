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

    public async Task<IEnumerable<TextResponse>> GetTextsForUser(string code)
    {
        var result = await _client
            .From<TextModel>()
            .Where(t => t.UserCode == code)
            .Get();

        return result.Models.Select(t => new TextResponse
        {
            Id = t.Id,
            UserCode = t.UserCode,
            Title = t.Title,
            Content = t.Content,
            LanguageFrom = t.LanguageFrom,
            LanguageTo = t.LanguageTo,
        });
    }

    public async Task<IEnumerable<object>> GetTextDetailsFromId(int id)
    {
        var result = await _client
            .From<TextModel>()
            .Where(t => t.Id == id)
            .Get();

        return result.Models.Select(t => new
        {
            t.Id,
            t.Content,
            t.LanguageFrom,
            t.LanguageTo,
            t.Title
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
