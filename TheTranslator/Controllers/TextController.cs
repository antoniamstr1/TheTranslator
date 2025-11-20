using Microsoft.AspNetCore.Mvc;
using TheTranslator.Services;
using TheTranslator.Models;

namespace TheTranslator.Controllers;

[ApiController]
[Route("texts")]
public class TextController : ControllerBase
{
    private readonly TextService _service;

    public TextController(TextService service)
    {
        _service = service;
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetTexts(string code)
    {
        var texts = await _service.GetTextsForUser(code);
        /* if (!texts.Any()) return NotFound(); */
        return Ok(texts);
    }

    [HttpGet("by-id/{id}")]
    public async Task<IActionResult> GetTextDetailsFromId(int id)
    {
        var texts = await _service.GetTextDetailsFromId(id);
        /* if (!texts.Any()) return NotFound(); -> ne želim da vraća 404 ako nema ništa, nego praznu listu*/
        return Ok(texts);
    }


    [HttpPost]
    public async Task<IActionResult> CreateText([FromBody] TextModel req)
    {
        var id = await _service.CreateText(req);
        return Ok(id);
    }

    [HttpDelete("{textId}")]
    public async Task<IActionResult> DeleteText(int textId)
    {
        await _service.DeleteText(textId);
        return NoContent();
    }
}
