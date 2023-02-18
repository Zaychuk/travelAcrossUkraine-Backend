namespace TravelAcrossUkraine.WebApi.Entities;

public class TypeEntity : BaseEntity
{
    public string Name { get; set; }
    public List<CategoryEntity> Categories { get; set; }
}
