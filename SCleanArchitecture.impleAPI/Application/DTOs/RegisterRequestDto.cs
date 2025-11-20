namespace SCleanArchitecture.SimpleAPI.Application.DTOs;

/// <summary>
/// DTO for user registration
/// Used when a new user creates an account
/// </summary>
public sealed class RegisterRequestDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    /// <summary>
    /// Validates the registration request
    /// Returns true if all fields are valid
    /// </summary>
    public bool IsValid()
    {
        // Check if name is provided
        if (string.IsNullOrWhiteSpace(Name))
        {
            return false;
        }

        // Check if email is provided and has basic email format
        if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
        {
            return false;
        }

        // Check if password is provided and meets minimum requirements
        if (string.IsNullOrWhiteSpace(Password))
        {
            return false;
        }

        // Password must be at least 6 characters
        if (Password.Length < 6)
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
        if (string.IsNullOrWhiteSpace(Name))
        {
            return "Name is required";
        }

        if (string.IsNullOrWhiteSpace(Email))
        {
            return "Email is required";
        }

        if (!Email.Contains("@"))
        {
            return "Email must be a valid email address";
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            return "Password is required";
        }

        if (Password.Length < 6)
        {
            return "Password must be at least 6 characters long";
        }

        return string.Empty;  // No errors = valid!
    }
}