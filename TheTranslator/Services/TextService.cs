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

    public async Task<Boolean> CheckIfTextExistsById(int id)
    {
        var response = await _client.From<TextModel>()
            .Where(x => x.Id == id)
            .Get();

        return response.Models.Any();

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


    public async Task<bool> UpdateText(int id, UpdateTextRequest req)
    {
        var response = await _client
        .From<TextModel>()
        .Where(t => t.Id == id)
        .Set(t => t.Content, req.Content)
        .Set(t => t.LanguageFrom, req.LanguageFrom)
        .Update();


        return response.Models.Any();
    }
}
