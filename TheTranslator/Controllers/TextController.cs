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

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetTexts(int userId)
    {
        var texts = await _service.GetTextsForUser(userId);
        if (!texts.Any()) return NotFound();
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
