namespace SCleanArchitecture.SimpleAPI.Application.DTOs;

public sealed class AddCategoryRequestDto
{
    public string Name { get; set; }
    public string Description { get; set; }

    public bool IsValid()
    {
        if (string.IsNullOrEmpty(Name))
        {
            return false;
        }

        return true;
    }
}