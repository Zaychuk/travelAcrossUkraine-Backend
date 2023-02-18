namespace TravelAcrossUkraine.WebApi.Entities;

public class CategoryEntity : BaseEntity
{
    public string Name { get; set; }
    public Guid TypeId { get; set; }
    public TypeEntity Type { get; set; }
    public List<LocationEntity> Locations { get; set; }
}
