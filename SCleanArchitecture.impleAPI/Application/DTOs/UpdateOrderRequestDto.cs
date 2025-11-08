using System.Linq;

namespace SCleanArchitecture.SimpleAPI.Application.DTOs;

public class UpdateOrderRequestDto
{
    public int Id { get; set; }
    public string Status { get; set; }  // Only allow status updates

    public bool IsValid()
    {
        if (Id <= 0)
            return false;

        if (string.IsNullOrEmpty(Status))
            return false;

        var validStatuses = new[] { "Pending", "Completed", "Cancelled" };
        if (!validStatuses.Contains(Status))
            return false;

        return true;
    }
}