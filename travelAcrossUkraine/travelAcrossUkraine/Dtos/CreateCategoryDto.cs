namespace TravelAcrossUkraine.WebApi.Dtos;

public class CreateCategoryDto
{
    public string Name { get; set; }
    public Guid TypeId { get; set; }
}
