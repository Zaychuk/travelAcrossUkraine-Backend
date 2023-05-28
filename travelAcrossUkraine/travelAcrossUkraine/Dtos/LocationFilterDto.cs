namespace TravelAcrossUkraine.WebApi.Dtos;

public class LocationFilterDto
{
    public string Term { get; set; }
    public Guid? CategoryId { get; set; }
}
