namespace TravelAcrossUkraine.WebApi.Dtos;

public class CollectionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<LocationDto> Locations { get; set; }
}
