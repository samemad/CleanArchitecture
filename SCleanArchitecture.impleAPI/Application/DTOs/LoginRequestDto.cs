namespace SCleanArchitecture.SimpleAPI.Application.DTOs;

/// <summary>
/// DTO for user login
/// Used when an existing user wants to authenticate
/// </summary>
public sealed class LoginRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }

    /// <summary>
    /// Validates the login request
    /// Returns true if both email and password are provided
    /// </summary>
    public bool IsValid()
    {
        // Check if email is provided
        if (string.IsNullOrWhiteSpace(Email))
        {
            return false;
        }

        // Check if password is provided
        if (string.IsNullOrWhiteSpace(Password))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Gets detailed validation errors for better user feedback
    /// Returns empty string if valid
    /// </summary>
    public string GetValidationErrors()
    {
        if (string.IsNullOrWhiteSpace(Email))
        {
            return "Email is required";
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            return "Password is required";
        }

        return string.Empty;  // No errors = valid!
    }
}