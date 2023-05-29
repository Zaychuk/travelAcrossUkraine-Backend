using TravelAcrossUkraine.WebApi.Dtos;

namespace TravelAcrossUkraine.WebApi.Helpers;

public static class AreaHelper
{

    public static bool CheckIfGeoPointInsidePolygon(PolygonDto poly, int n, GeoPointDto p)
    {

        // When polygon has less than 3 edge, it is not polygon
        if (n < 3)
            return false;

        // Create a point at infinity, y is same as point p
        var pt = new GeoPointDto
        {
            CoordinateY = p.CoordinateY,
            CoordinateX = decimal.MaxValue / 10000000,
        };
        var exline = new LineDto(p, pt);
        int count = 0;
        int i = 0;
        do
        {
            // Forming a line from two consecutive points of
            // poly
            var side = new LineDto(poly.GeoPoints.Single(p => p.SequenceNumber == i), poly.GeoPoints.Single(p => p.SequenceNumber == (i + 1) % n));
            if (IsIntersect(side, exline))
            {

                // If side is intersects exline
                if (GetDirection(side.FirstPoint, p, side.SecondPoint) == 0)
                    return IsOnLine(side, p);
                count++;
            }
            i = (i + 1) % n;
        } while (i != 0);

        // When count is odd
        return (count % 2) == 1;
    }

    public static bool CheckIfPolygonsIntersect(PolygonDto polygon1, PolygonDto polygon2)
    {
        var result = false;
        for (int i = 0; i < polygon1.GeoPoints.Count - 1; i++)
        {
            for (int j = 0; j < polygon2.GeoPoints.Count - 1; j++)
            {
                if(IsIntersect(new LineDto(polygon1.GeoPoints[i], polygon1.GeoPoints[i + 1]), new LineDto(polygon2.GeoPoints[j], polygon2.GeoPoints[j + 1])))
                {
                    result = true;
                    break;
                }
            }
            if(result)
            {
                break;
            }
        }

        return result;
    }

    public static bool CheckIfPolygonAndCirleIntersect(PolygonDto polygon, CircleDto cirlce)
    {
        if (CheckIfGeoPointInsidePolygon(polygon, polygon.GeoPoints.Count, cirlce.CenterGeoPoint))
        {
            return true;
        }
        var result = false;
        for (int i = 0; i < polygon.GeoPoints.Count - 1; i++)
        {
            var distance = DistanceFromPointToLine(cirlce.CenterGeoPoint, new LineDto(polygon.GeoPoints[i], polygon.GeoPoints[i + 1]));
            if ((decimal)distance > cirlce.Radius)
            {
                result = true;
                break;
            }
        }

        return result;
    }

    //private static GeoPointDto GetClosestPoint(GeoPointDto a, GeoPointDto b, GeoPointDto p)
    //{
    //    var vectorAP = (p.CoordinateX - a.CoordinateX, p.CoordinateY - a.CoordinateY);     //Vector from A to P
    //    var vectorAB = (b.CoordinateX - a.CoordinateX, b.CoordinateY - a.CoordinateY);     //Vector from A to B

    //    var magnitudeAB = vectorAB.Item1 * vectorAB.Item1 + vectorAB.Item2 * vectorAB.Item2;
    //    //Magnitude of AB vector (it's length)


    //    var ABAPproduct = vectorAB.Item1 * vectorAP.Item1 + vectorAB.Item2 * vectorAP.Item2;
    //    //The product of a_to_p and a_to_b


    //    var distance = ABAPproduct / magnitudeAB;
    //    //The normalized "distance" from a to your closest point

    //    if (distance < 0)     //Check if P projection is over vectorAB
    //    {
    //        return new GeoPointDto(a.CoordinateX, a.CoordinateY);
    //    }
    //    else if (distance > magnitudeAB)
    //    {
    //        return new GeoPointDto(b.CoordinateX, b.CoordinateY);
    //    }
    //    else
    //    {
    //        return new GeoPointDto(a.CoordinateX + vectorAB.Item1 * distance, a.CoordinateY + vectorAB.Item2 * distance);
    //    }

    //}

    public static double DistanceFromPointToLine(GeoPointDto point, LineDto line)
    {
        GeoPointDto l1 = line.FirstPoint;
        GeoPointDto l2 = line.SecondPoint;

        return decimal.ToDouble(Math.Abs((l2.CoordinateX - l1.CoordinateX) * (l1.CoordinateY - point.CoordinateY) - (l1.CoordinateX - point.CoordinateX) * (l2.CoordinateY - l1.CoordinateY))) /
                Math.Sqrt(Math.Pow(decimal.ToDouble(l2.CoordinateX - l1.CoordinateX), 2) + Math.Pow(decimal.ToDouble(l2.CoordinateY - l1.CoordinateY), 2));
    }

    private static bool IsOnLine(LineDto l1, GeoPointDto p)
    {
        // Check whether p is on the line or not
        return p.CoordinateX <= Math.Max(l1.FirstPoint.CoordinateX, l1.SecondPoint.CoordinateX)
            && p.CoordinateX <= Math.Min(l1.FirstPoint.CoordinateX, l1.SecondPoint.CoordinateX)
            && p.CoordinateY <= Math.Max(l1.FirstPoint.CoordinateY, l1.SecondPoint.CoordinateY)
            && p.CoordinateY <= Math.Min(l1.FirstPoint.CoordinateY, l1.SecondPoint.CoordinateY);
    }

    private static int GetDirection(GeoPointDto a, GeoPointDto b, GeoPointDto c)
    {
        var val = (b.CoordinateY - a.CoordinateY) * (c.CoordinateX - b.CoordinateX)
          - (b.CoordinateX - a.CoordinateX) * (c.CoordinateY - b.CoordinateY);

        if (val == 0)

            // Collinear
            return 0;

        else if (val < 0)

            // Anti-clockwise direction
            return 2;

        // Clockwise direction
        return 1;
    }

    private static bool IsIntersect(LineDto l1, LineDto l2)
    {
        // Four direction for two lines and points of other line
        int dir1 = GetDirection(l1.FirstPoint, l1.SecondPoint, l2.FirstPoint);
        int dir2 = GetDirection(l1.FirstPoint, l1.SecondPoint, l2.SecondPoint);
        int dir3 = GetDirection(l2.FirstPoint, l2.SecondPoint, l1.FirstPoint);
        int dir4 = GetDirection(l2.FirstPoint, l2.SecondPoint, l1.SecondPoint);

        // When intersecting
        if (dir1 != dir2 && dir3 != dir4)
            return true;

        // When p2 of line2 are on the line1
        if (dir1 == 0 && IsOnLine(l1, l2.FirstPoint))
            return true;

        // When p1 of line2 are on the line1
        if (dir2 == 0 && IsOnLine(l1, l2.SecondPoint))
            return true;

        // When p2 of line1 are on the line2
        if (dir3 == 0 && IsOnLine(l2, l1.FirstPoint))
            return true;

        // When p1 of line1 are on the line2
        if (dir4 == 0 && IsOnLine(l2, l1.SecondPoint))
            return true;

        return false;
    }
}
