namespace travelAcrossUkraine.WebApi.Dtos;

public class GeoPointDto
{
    public Guid Id { get; set; }
    public decimal CoordinateX { get; set; }
    public decimal CoordinateY { get; set; }
    public int? SequenceNumber { get; set; }
}
