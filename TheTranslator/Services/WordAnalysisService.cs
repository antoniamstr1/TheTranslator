using TheTranslator.Contracts;
using System.Net.Http.Json;
using System.Text.Json;
using System.Runtime.CompilerServices;

namespace TheTranslator.Services;

public class WordAnalysisService
{
    private readonly HttpClient _httpsClient;

    public WordAnalysisService(HttpClient httpsClient)
    {
        _httpsClient = httpsClient;
    }

    public async Task<WordAnalysisResponse> GetWordAnalysis(string language, string word)
    {
        var url = $"{Environment.GetEnvironmentVariable("WORD_API_URL")}{language}/{word}?translations=true";

        try
        {
            //var response = await _httpsClient.GetFromJsonAsync<WordAnalysisResponse>(url);
            //return response;

            var json = await _httpsClient.GetStringAsync(url);

            //deserijaliziran json
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            var entries = root.GetProperty("entries");
            if (!entries.EnumerateArray().Any())
                return null;


            // 1. field
            var partOfSpeech = entries[0].GetProperty("partOfSpeech").GetString() ?? string.Empty;
            // 2. field
            var formsList = entries[0].GetProperty("forms");
            WordForm forms = new WordForm(); // defaultno stanje
            if (formsList.GetArrayLength() > 0)
            {
                var formsData = formsList[0];
                forms = new WordForm
                {
                    Word = formsData.GetProperty("word").GetString() ?? string.Empty,
                    Tags = formsData.GetProperty("tags").EnumerateArray()
                                .Select(t => t.GetString() ?? "")
                                .ToList()
                };
            }
            // 3. field
            var definitions = entries[0]
                      .GetProperty("senses")
                      .EnumerateArray()
                      .Select(s => s.GetProperty("definition").GetString() ?? string.Empty)
                      .Where(d => d != null)
                      .ToList();

            var response = new WordAnalysisResponse
            {
                Type = partOfSpeech,
                Forms = forms,
                Definitions = definitions
            };
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching word analysis: {ex.Message}");
            return null;
        }
    }
}
