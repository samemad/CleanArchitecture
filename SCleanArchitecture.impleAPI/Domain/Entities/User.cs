namespace SCleanArchitecture.SimpleAPI.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    // NEVER store plain text passwords!
    public string PasswordHash { get; set; }


    // The database will set this when we save
    public DateTime CreatedAt { get; set; }
}
