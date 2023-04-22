namespace TravelAcrossUkraine.WebApi.Entities.BaseEntities;

public class BaseEntity : SoftDeleteBaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
