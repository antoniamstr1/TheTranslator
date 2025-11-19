using Microsoft.AspNetCore.Mvc;
using TheTranslator.Services;
using TheTranslator.Models;

namespace TheTranslator.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly UserService _service;

    public UserController(UserService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser()
    {
        var code = await _service.CreateUser();
        return Ok(code);
    }

    [HttpGet("check-code/{code}")]
    public async Task<IActionResult> Check(string code)
    {
        var isCheckPassed = await _service.CheckCode(code);
        return Ok(isCheckPassed);
    }
}
