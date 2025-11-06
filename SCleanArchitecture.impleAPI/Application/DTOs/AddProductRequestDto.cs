namespace SCleanArchitecture.SimpleAPI.Application.DTOs;

public sealed class AddProductRequestDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }

    public bool IsValid()
    {
        if (string.IsNullOrEmpty(Name))
        {
            return false;
        }

        if (Price <= 0)
        {
            return false;
        }

        if (Stock < 0)
        {
            return false;
        }

        if (CategoryId <= 0)
        {
            return false;
        }

        return true;
    }
}
