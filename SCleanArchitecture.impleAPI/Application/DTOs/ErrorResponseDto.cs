namespace SCleanArchitecture.SimpleAPI.Application.DTOs;

/// <summary>
/// Standard error response format
/// Makes error handling consistent across all endpoints
/// </summary>
public class ErrorResponseDto
{
    /// <summary>
    /// Error message for the user
    /// Example: "Invalid email or password"
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Detailed error information (optional)
    /// Use for development/debugging
    /// Example: "User with email john@example.com not found"
    /// </summary>
    public string Details { get; set; }

    /// <summary>
    /// HTTP status code
    /// Example: 400 (Bad Request), 401 (Unauthorized), 404 (Not Found)
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Timestamp when error occurred
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Constructor for quick error creation
    /// </summary>
    public ErrorResponseDto()
    {
        Timestamp = DateTime.UtcNow;
    }

    /// <summary>
    /// Create error response with message and status code
    /// </summary>
    public ErrorResponseDto(string message, int statusCode) : this()
    {
        Message = message;
        StatusCode = statusCode;
    }

    /// <summary>
    /// Create error response with message, details, and status code
    /// </summary>
    public ErrorResponseDto(string message, string details, int statusCode) : this()
    {
        Message = message;
        Details = details;
        StatusCode = statusCode;
    }
}