namespace TravelAcrossUkraine.WebApi.Dtos;

public class LineDto
{
    public GeoPointDto FirstPoint, SecondPoint;

    public LineDto(GeoPointDto p1, GeoPointDto p2)
    {
        FirstPoint = p1;
        SecondPoint = p2;
    }
}
