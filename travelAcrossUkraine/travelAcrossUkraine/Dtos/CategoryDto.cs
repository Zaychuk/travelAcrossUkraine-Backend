namespace TravelAcrossUkraine.WebApi.Dtos;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public TypeWithoutCategoryDto Type { get; set; }
}


