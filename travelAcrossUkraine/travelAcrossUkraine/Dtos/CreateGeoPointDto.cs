namespace TravelAcrossUkraine.WebApi.Dtos;

public class CreateGeoPointDto
{
    public decimal CoordinateX { get; set; }
    public decimal CoordinateY { get; set; }
    public int? SequenceNumber { get; set; }
}
