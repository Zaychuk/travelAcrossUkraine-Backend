namespace TravelAcrossUkraine.WebApi.Dtos;

public class CreateCircleDto
{
    public CreateGeoPointDto CenterGeoPoint { get; set; }
    public decimal Radius { get; set; }
}
