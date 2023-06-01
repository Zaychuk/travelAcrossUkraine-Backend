namespace TravelAcrossUkraine.WebApi.Dtos;

public class AddLocationToCollectionsDto
{
    public List<Guid> CollectionIds { get; set; }
    public Guid LocationId { get; set; }
}
