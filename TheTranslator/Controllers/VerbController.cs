using Microsoft.AspNetCore.Mvc;
using TheTranslator.Services;
using TheTranslator.Models;

namespace TheTranslator.Controllers;

[ApiController]
[Route("verb-conjugation")]
public class VerbController : ControllerBase
{
    private readonly VerbConjugationService _service;

    public VerbController(VerbConjugationService service)
    {
        _service = service;
    }

    //italian-ita
    //spanish-spa
    [HttpGet("{language}/{verb}")]
    [HttpHead("{language}/{verb}")]
    public async Task<IActionResult> GetConjugation(string language, string verb)
    {
        var conjugations = await _service.GetConjugationByVerb(language, verb);
        if (!conjugations.Any()) return NotFound();
        return Ok(conjugations);
    }

}
