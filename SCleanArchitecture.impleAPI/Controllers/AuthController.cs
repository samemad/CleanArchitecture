using Microsoft.AspNetCore.Mvc;
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Application.Services;

namespace SCleanArchitecture.SimpleAPI.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Register a new user
    /// POST /auth/register
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
    {
        try
        {
            var response = await _authService.RegisterAsync(requestDto);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            // Validation errors or user already exists
            return BadRequest(new ErrorResponseDto(ex.Message, 400));
        }
        catch (Exception ex)
        {
            // Unexpected errors
            return StatusCode(500, new ErrorResponseDto("An error occurred during registration", ex.Message, 500));
        }
    }

    /// <summary>
    /// Login existing user
    /// POST /auth/login
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
    {
        try
        {
            var response = await _authService.LoginAsync(requestDto);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            // Invalid credentials
            return Unauthorized(new ErrorResponseDto(ex.Message, 401));
        }
        catch (InvalidOperationException ex)
        {
            // Validation errors
            return BadRequest(new ErrorResponseDto(ex.Message, 400));
        }
        catch (Exception ex)
        {
            // Unexpected errors
            return StatusCode(500, new ErrorResponseDto("An error occurred during login", ex.Message, 500));
        }
    }
}