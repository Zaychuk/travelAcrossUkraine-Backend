using TravelAcrossUkraine.WebApi.Entities.BaseEntities;

namespace TravelAcrossUkraine.WebApi.Entities;

public class ImageEntity : BaseEntity
{
    public string Url { get; set; }
    public LocationEntity Location { get; set; }
    public Guid LocationId { get; set; }
}
