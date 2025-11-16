using Microsoft.AspNetCore.Mvc;
using TheTranslator.Services;
using System.Globalization;
using System.Text;

namespace TheTranslator.Controllers;

[ApiController]
[Route("word-analysis")]
public class WordAnalysisController : ControllerBase
{
    private readonly WordAnalysisService _WAService;
    private readonly VerbConjugationService _VCService;

    public WordAnalysisController(WordAnalysisService WAService, VerbConjugationService VCservice)
    {
        _WAService = WAService;
        _VCService = VCservice;
    }

    //italian-it
    //spanish-es
    [HttpGet("{language}/{word}")]
    public async Task<IActionResult> GetAnalysis(string language, string word)
    {
        var analysis = await _WAService.GetWordAnalysis(language, word);
        if (analysis == null) return NotFound();
        Console.WriteLine(analysis);
        //pozvati konjugaciju ako je glagol (partOfSpeech: verb)
        if (analysis.Type == "verb")
        {
            var infinitive = "";
            //ako je mangiar ili mangio: form je prazan i uzimam glagog kao zadnju riječ iz definicije
            if (analysis.Forms.Tags.Count == 0)
            {
                infinitive = analysis.Definitions[0].Split(' ')[^1];
            }
            // ako je infinitiv mangiare, u forms je tags ["canonical"] i uzimam word
            else if (analysis.Forms.Tags[0] == "canonical")
            {
                var infinitive_non_normalized = analysis.Forms.Word;
                infinitive = infinitive_non_normalized.Normalize(NormalizationForm.FormD);
                infinitive = new string(infinitive.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());
            }

            // u canonical form su dijaklikticki znakovi koje treba maknuti 
            if (string.IsNullOrWhiteSpace(infinitive))
                return BadRequest("Cannot determine the infinitive form of the verb.");

            var verbConjugation = await _VCService.GetConjugationByVerb(language, infinitive);

            //ako je i glagol onda se vraća i konjugacija, ako nije onda samo analiza
            if (analysis == null) return NotFound();
            return Ok(new { Analysis = analysis, Conjugation = verbConjugation });
        }

        return Ok(new{Analysis = analysis} );
    }

}
