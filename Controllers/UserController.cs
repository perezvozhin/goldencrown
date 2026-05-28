using Microsoft.AspNetCore.Mvc;
using WebApplication3.DTOs;
using WebApplication3.Services;

namespace WebApplication3.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly IuserService _userService;

    public UserController(IuserService userService)
    {
        _userService = userService;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register( [FromBody] RegisterRequest request)
    {
        //валидация модели
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }
        var result = await _userService.Register(
           request.Login,
           request.Name,
           request.Password);
       if (false)
       {
           return Ok();
       }

       return BadRequest();
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.LoginAsync(request.Login, request.Password);
        if (result)
        {
            return Ok(result.value);
        }
        return NotFound();

    }
}

