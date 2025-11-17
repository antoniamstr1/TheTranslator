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
    [HttpHead("{language}/{word}")]
    public async Task<IActionResult> GetAnalysis(string language, string word)
    {
        var analysis = await _WAService.GetWordAnalysis(language, word);
        if (analysis == null) return NotFound();
        Console.WriteLine(analysis);
        //pozvati konjugaciju ako je glagol (partOfSpeech: verb)
        if (analysis.Type == "verb")
        {
            var infinitive = "";
            // ako je infinitiv mangiare, u forms je tags ["canonical"] i uzimam word
            if (analysis.Forms.Tags != null && analysis.Forms.Tags.Count > 0 && analysis.Forms.Tags[0] == "canonical")
            {
                var infinitive_non_normalized = analysis.Forms.Word;
                infinitive = infinitive_non_normalized.Normalize(NormalizationForm.FormD);
                infinitive = new string(infinitive.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());
            }
            //ako je mangiar ili mangio: form je prazan i uzimam glagog kao zadnju riječ iz definicije
            else
            {
                // može biti zadnja riječ i može bitti riječ koja završava s :
                var words = analysis.Definitions[0].Split(' ');
                var wordWithColon = words.FirstOrDefault(w => w.EndsWith(":"));

                // If found, remove the colon, otherwise take the last word
                infinitive = (wordWithColon ?? words[^1]).TrimEnd(':', '.');
            }

            if (string.IsNullOrWhiteSpace(infinitive))
                return BadRequest("Cannot determine the infinitive form of the verb.");

            var verbConjugation = await _VCService.GetConjugationByVerb(language, infinitive);

            //ako je i glagol onda se vraća i konjugacija, ako nije onda samo analiza
            if (analysis == null) return NotFound();
            return Ok(new { Analysis = analysis, Conjugation = verbConjugation });
        }

        return Ok(new { Analysis = analysis });
    }



}
