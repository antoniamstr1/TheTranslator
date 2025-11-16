using Microsoft.AspNetCore.Mvc;
using TheTranslator.Services;
using TheTranslator.Models;

namespace TheTranslator.Controllers;


[ApiController]
[Route("word")]
public class WordController : ControllerBase
{
    private readonly MarkedWordService _service;

    public WordController(MarkedWordService service)
    {
        _service = service;
    }

    [HttpGet("/{textId}")]
    public async Task<IActionResult> GetWords(int textId)
    {
        var words = await _service.GetWordsForText(textId);
        if (!words.Any()) return NotFound();
        return Ok(words);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWord([FromBody] MarkedWordModel req)
    {
        var id = await _service.CreateWord(req);
        return Ok(id);
    }

    [HttpPut("{wordId}")]
    public async Task<IActionResult> UpdateWord(int wordId, [FromBody] MarkedWordModel req)
    {
        var id = await _service.UpdateWord(wordId, req.IsPinned, req.Level);
        if (id is null) return NotFound();
        return Ok(id);
    }

    [HttpDelete("{wordId}")]
    public async Task<IActionResult> DeleteWord(int wordId)
    {
        await _service.DeleteWord(wordId);
        return NoContent();
    }

    [HttpGet("review/{userCode}")]
    public async Task<IActionResult> Review(string userCode)
    {
        var words = await _service.GetWordsForReview(userCode);
        if (!words.Any()) return NotFound();
        return Ok(words);
    }
}
