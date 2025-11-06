namespace SCleanArchitecture.SimpleAPI.Application.DTOs;

public class UpdateProductRequestDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }

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