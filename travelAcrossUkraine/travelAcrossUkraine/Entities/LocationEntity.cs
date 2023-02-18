namespace TravelAcrossUkraine.WebApi.Entities;

public class LocationEntity : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ImageEntity> Images { get; set; }
    public string? WikipediaUrl { get; set; }
    public string? PetitionUrl { get; set; }
    public CategoryEntity Category { get; set; }
    public Guid CategoryId { get; set; }
}
