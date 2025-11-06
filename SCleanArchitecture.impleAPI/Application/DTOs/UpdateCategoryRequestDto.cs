namespace SCleanArchitecture.SimpleAPI.Application.DTOs;

public class UpdateCategoryRequestDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public bool IsValid()
    {
        if (Id <= 0)
        {
            return false;
        }

        if (string.IsNullOrEmpty(Name))
        {
            return false;
        }

        return true;
    }
}