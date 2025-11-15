using Supabase;
using TheTranslator.Models;
using TheTranslator.Contracts;
using TheTranslator.Enums;

namespace TheTranslator.Services;


public class WordService
{
    private readonly Client _client;

    public WordService(Client client)
    {
        _client = client;
    }

    public async Task<IEnumerable<WordResponse>> GetWordsForText(int textId)
    {
        var result = await _client
            .From<WordModel>()
            .Where(w => w.TextId == textId)
            .Get();

        return result.Models.Select(w => new WordResponse
        {
            Id = w.Id,
            Word = w.Word,
            IsPinned = w.IsPinned,
            Level = w.Level,
        });
    }

    public async Task<int> CreateWord(WordModel req)
    {
        var response = await _client.From<WordModel>().Insert(req);
        return response.Models.First().Id;
    }

    public async Task<int?> UpdateWord(int id, bool isPinned, WordLevel level)
    {
        var response = await _client
            .From<WordModel>()
            .Where(w => w.Id == id)
            .Set(w => w.IsPinned, isPinned)
            .Set(w => w.Level, level)
            .Update();

        return response.Models.FirstOrDefault()?.Id;
    }

    public async Task DeleteWord(int id)
    {
        await _client
            .From<WordModel>()
            .Where(w => w.Id == id)
            .Delete();
    }

    public async Task<IEnumerable<WordResponse>> GetWordsForReview(string userCode)
    {
        var result = await _client
            .From<WordModel>()
            .Where(w => w.UserCode == userCode)
            .Get();

        return result.Models.Select(w => new WordResponse
        {
            Id = w.Id,
            Word = w.Word,
            Level = w.Level,
        });
    }
}
