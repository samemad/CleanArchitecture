using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SCleanArchitecture.SimpleAPI.Application.Services;

/// <summary>
/// Service responsible for generating and validating JWT tokens
/// </summary>
public interface ITokenService
{
    string GenerateToken(int userId, string email, string name);
    ClaimsPrincipal? ValidateToken(string token);
}

internal sealed class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Generates a JWT token for authenticated user
    /// </summary>
    public string GenerateToken(int userId, string email, string name)
    {
        // Get JWT settings from appsettings.json
        var jwtKey = _configuration["Jwt:Key"];
        var jwtIssuer = _configuration["Jwt:Issuer"];
        var jwtAudience = _configuration["Jwt:Audience"];
        var jwtExpirationMinutes = int.Parse(_configuration["Jwt:ExpirationMinutes"]);

        // Create security key from secret
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Create claims (user information stored in token)
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),  // User ID
            new Claim(JwtRegisteredClaimNames.Email, email),             // Email
            new Claim(JwtRegisteredClaimNames.Name, name),               // Name
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  // Unique token ID
        };

        // Create token
        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtExpirationMinutes),
            signingCredentials: credentials
        );

        // Return token as string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// Validates a JWT token and returns the claims
    /// </summary>
    public ClaimsPrincipal? ValidateToken(string token)
    {
        var jwtKey = _configuration["Jwt:Key"];
        var jwtIssuer = _configuration["Jwt:Issuer"];
        var jwtAudience = _configuration["Jwt:Audience"];

        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            return principal;
        }
        catch
        {
            return null;  // Token is invalid
        }
    }
}