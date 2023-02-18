namespace TravelAcrossUkraine.WebApi.Dtos;

public class CreateCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid TypeId { get; set; }
}
