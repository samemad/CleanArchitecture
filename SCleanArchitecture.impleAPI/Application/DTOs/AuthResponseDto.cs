namespace SCleanArchitecture.SimpleAPI.Application.DTOs;

/// <summary>
/// DTO returned after successful login or registration
/// Contains the JWT token and user information
/// </summary>
public class AuthResponseDto
{
    /// <summary>
    /// JWT token - the client must include this in future requests
    /// Example: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// User's email address
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// User's display name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// User's unique ID in the database
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// When the token expires (optional, but helpful for client)
    /// Client should refresh token before this time
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Success message (optional)
    /// Example: "Login successful" or "Registration successful"
    /// </summary>
    public string Message { get; set; }
}