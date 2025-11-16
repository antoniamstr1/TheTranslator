using Supabase;
using TheTranslator.Models;
using TheTranslator.Contracts;
using TheTranslator.Enums;

namespace TheTranslator.Services;


public class MarkedWordService
{
    private readonly Client _client;

    public MarkedWordService(Client client)
    {
        _client = client;
    }

    public async Task<IEnumerable<MarkedWordResponse>> GetWordsForText(int textId)
    {
        var result = await _client
            .From<MarkedWordModel>()
            .Where(w => w.TextId == textId)
            .Get();

        return result.Models.Select(w => new MarkedWordResponse
        {
            Id = w.Id,
            Word = w.Word,
            IsPinned = w.IsPinned,
            Level = w.Level,
        });
    }

    public async Task<int> CreateWord(MarkedWordModel req)
    {
        var response = await _client.From<MarkedWordModel>().Insert(req);
        return response.Models.First().Id;
    }

    public async Task<int?> UpdateWord(int id, bool isPinned, WordLevel level)
    {
        var response = await _client
            .From<MarkedWordModel>()
            .Where(w => w.Id == id)
            .Set(w => w.IsPinned, isPinned)
            .Set(w => w.Level, level)
            .Update();

        return response.Models.FirstOrDefault()?.Id;
    }

    public async Task DeleteWord(int id)
    {
        await _client
            .From<MarkedWordModel>()
            .Where(w => w.Id == id)
            .Delete();
    }

    public async Task<IEnumerable<MarkedWordResponse>> GetWordsForReview(string userCode)
    {
        var result = await _client
            .From<MarkedWordModel>()
            .Where(w => w.UserCode == userCode)
            .Get();

        return result.Models.Select(w => new MarkedWordResponse
        {
            Id = w.Id,
            Word = w.Word,
            Level = w.Level,
        });
    }
}
