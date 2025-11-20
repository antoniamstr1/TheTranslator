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

    [HttpGet("check/{id}")]
    public async Task<IActionResult> CheckIfTextExists(int id)
    {
        var itExists = await _service.CheckIfTextExistsById(id);
        /* if (!texts.Any()) return NotFound(); -> ne želim da vraća 404 ako nema ništa, nego praznu listu*/
        return Ok(itExists);
    }


    [HttpPost]
    public async Task<IActionResult> CreateText([FromBody] TextModel req)
    {
        var id = await _service.CreateText(req);
        return Ok(id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateText(int id, [FromBody] UpdateTextRequest req)
    {
        var updated = await _service.UpdateText(id, req);

        if (!updated)
            return NotFound(); // text with that id doesn't exist

        return NoContent(); // success, no response body needed
    }


    [HttpDelete("{textId}")]
    public async Task<IActionResult> DeleteText(int textId)
    {
        await _service.DeleteText(textId);
        return NoContent();
    }
}
