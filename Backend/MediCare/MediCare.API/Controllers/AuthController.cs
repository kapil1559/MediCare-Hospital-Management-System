using Microsoft.AspNetCore.Mvc;
using MediCare.Core.Interfaces;
using MediCare.Domain.Requests;

namespace MediCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _userRepository.LoginAsync(request);

        if (result == null)
            return Unauthorized("Invalid Username or Password");

        return Ok(result);
    }
}