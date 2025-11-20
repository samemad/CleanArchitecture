using BCrypt.Net;
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Domain.Entities;
using SCleanArchitecture.SimpleAPI.Domain.Repositories;

namespace SCleanArchitecture.SimpleAPI.Application.Services;

/// <summary>
/// Service responsible for user authentication (register/login)
/// </summary>
public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto requestDto);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto requestDto);
}

internal sealed class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto requestDto)
    {
        // 1. Validate request
        if (!requestDto.IsValid())
        {
            throw new InvalidOperationException(requestDto.GetValidationErrors());
        }

        // 2. Check if user already exists
        var existingUsers = await _userRepository.GetAllUsersAsync();
        var userExists = existingUsers.Any(u => u.Email.ToLower() == requestDto.Email.ToLower());

        if (userExists)
        {
            throw new InvalidOperationException("User with this email already exists");
        }

        // 3. Hash the password using BCrypt
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(requestDto.Password);

        // 4. Create new user entity
        var user = new User
        {
            Name = requestDto.Name,
            Email = requestDto.Email,
            PasswordHash = passwordHash
        };

        // 5. Save user to database
        await _userRepository.AddUserAsync(user);

        // 6. Generate JWT token
        var token = _tokenService.GenerateToken(user.Id, user.Email, user.Name);

        // 7. Return response with token
        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email,
            Name = user.Name,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddHours(24),  // Token expires in 24 hours
            Message = "Registration successful"
        };
    }

    /// <summary>
    /// Login existing user
    /// </summary>
    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto requestDto)
    {
        // 1. Validate request
        if (!requestDto.IsValid())
        {
            throw new InvalidOperationException(requestDto.GetValidationErrors());
        }

        // 2. Find user by email
        var allUsers = await _userRepository.GetAllUsersAsync();
        var user = allUsers.FirstOrDefault(u => u.Email.ToLower() == requestDto.Email.ToLower());

        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        // 3. Verify password using BCrypt
        var isPasswordValid = BCrypt.Net.BCrypt.Verify(requestDto.Password, user.PasswordHash);

        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        // 4. Generate JWT token
        var token = _tokenService.GenerateToken(user.Id, user.Email, user.Name);

        // 5. Return response with token
        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email,
            Name = user.Name,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddHours(24),
            Message = "Login successful"
        };
    }
}