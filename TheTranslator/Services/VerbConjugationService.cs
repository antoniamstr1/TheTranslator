using TheTranslator.Contracts;
using TheTranslator.Models;

namespace TheTranslator.Services;

public class VerbConjugationService
{
    private readonly HttpClient _httpsClient;

    public VerbConjugationService(HttpClient httpsClient)
    {
        _httpsClient = httpsClient;
    }

    public async Task<VerbConjugations> GetConjugationByVerb(string language, string verb)
    {
        var url = $"{Environment.GetEnvironmentVariable("VERB_API_URL")}/verb?language={language}&verb={verb}";

        try
        {
            var response = await _httpsClient.GetFromJsonAsync<VerbConjugations>(url);

            return response ?? new VerbConjugations();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching conjugation: {verb}");
            return new VerbConjugations();
        }
    }
}
