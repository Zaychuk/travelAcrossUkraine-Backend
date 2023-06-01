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
        for (int i = 0; i < polygon1.GeoPoints.Count - 1; i++)
        {
            for (int j = 0; j < polygon2.GeoPoints.Count - 1; j++)
            {
                if (IsIntersect(new LineDto(polygon1.GeoPoints.Single(gp => gp.SequenceNumber == i), polygon1.GeoPoints.Single(gp => gp.SequenceNumber == i + 1)), new LineDto(polygon2.GeoPoints.Single(gp => gp.SequenceNumber == j), polygon2.GeoPoints.Single(gp => gp.SequenceNumber == j + 1))))
                {
                    return true;
                }
            }
        }
        for (int i = 0; i < polygon1.GeoPoints.Count - 1; i++)
        {
            if (CheckIfGeoPointInsidePolygon(polygon2, polygon2.GeoPoints.Count - 1, polygon1.GeoPoints.Single(gp => gp.SequenceNumber == i)))
            {
                return true;
            }
        }
        for (int i = 0; i < polygon2.GeoPoints.Count - 1; i++)
        {
            if (CheckIfGeoPointInsidePolygon(polygon1, polygon1.GeoPoints.Count - 1, polygon2.GeoPoints.Single(gp => gp.SequenceNumber == i)))
            {
                return true;
            }
        }


        return false;
    }

    public static bool CheckIfPolygonAndCirleIntersect(PolygonDto polygon, CircleDto cirlce)
    {
        if (CheckIfGeoPointInsidePolygon(polygon, polygon.GeoPoints.Count - 1, cirlce.CenterGeoPoint))
        {
            return true;
        }
        var result = false;
        for (int i = 0; i < polygon.GeoPoints.Count - 1; i++)
        {
            var distance = DistanceFromPointToLine(cirlce.CenterGeoPoint, new LineDto(polygon.GeoPoints.Single(gp => gp.SequenceNumber == i), polygon.GeoPoints.Single(gp => gp.SequenceNumber == i + 1)));
            if ((decimal)distance <= cirlce.Radius)
            {
                result = true;
                break;
            }
        }

        return result;
    }

    public static bool CheckIfTwoCirlesIntersect(CircleDto circle1, CircleDto circle2)
    {
        var distanceBetweenTwoCenters = DistanceBetweenPoints(circle1.CenterGeoPoint, circle2.CenterGeoPoint);

        return distanceBetweenTwoCenters <= circle1.Radius + circle2.Radius;
    }

    public static bool CheckIsPointInsideCircle(GeoPointDto point, CircleDto circle)
    {
        var distance = DistanceBetweenPoints(point, circle.CenterGeoPoint);

        return distance <= circle.Radius;
    }

    private static decimal DistanceBetweenPoints(GeoPointDto point1, GeoPointDto point2)
    {
        return (decimal)Math.Sqrt(
            Math.Pow(decimal.ToDouble(point2.CoordinateX) - decimal.ToDouble(point1.CoordinateX), 2)
            +
            Math.Pow(decimal.ToDouble(point2.CoordinateY) - decimal.ToDouble(point1.CoordinateY), 2));
    }

    //private static double DistanceFromPointToLine(GeoPointDto point, LineDto line)
    //{
    //    GeoPointDto l1 = line.FirstPoint;
    //    GeoPointDto l2 = line.SecondPoint;

    //    return decimal.ToDouble(Math.Abs((l2.CoordinateX - l1.CoordinateX) * (l1.CoordinateY - point.CoordinateY) - (l1.CoordinateX - point.CoordinateX) * (l2.CoordinateY - l1.CoordinateY))) /
    //            Math.Sqrt(Math.Pow(decimal.ToDouble(l2.CoordinateX - l1.CoordinateX), 2) + Math.Pow(decimal.ToDouble(l2.CoordinateY - l1.CoordinateY), 2));
    //}

    public static double DistanceFromPointToLine(GeoPointDto point, LineDto line)
    {
        double lineLength = CalculateDistance(line.FirstPoint, line.SecondPoint);

        if (lineLength == 0)
        {
            // The line has zero length, so return the distance between the point and the start of the line
            return CalculateDistance(point, line.FirstPoint);
        }

        // Calculate the projection of the point onto the line
        double t = (decimal.ToDouble((point.CoordinateX - line.FirstPoint.CoordinateX) * (line.SecondPoint.CoordinateX - line.FirstPoint.CoordinateX) +
                    (point.CoordinateY - line.FirstPoint.CoordinateY) * (line.SecondPoint.CoordinateY - line.FirstPoint.CoordinateY))) / (lineLength * lineLength);

        // Clamp the parameter 't' to ensure the projection is within the line segment
        t = Math.Max(0, Math.Min(1, t));

        // Calculate the closest point on the line to the given point
        double closestX = decimal.ToDouble(line.FirstPoint.CoordinateX) + t * decimal.ToDouble((line.SecondPoint.CoordinateX - line.FirstPoint.CoordinateX));
        double closestY = decimal.ToDouble(line.FirstPoint.CoordinateY) + t * decimal.ToDouble((line.SecondPoint.CoordinateY - line.FirstPoint.CoordinateY));

        // Calculate the distance between the closest point and the given point
        return CalculateDistance(point, new GeoPointDto { CoordinateX = (decimal)closestX, CoordinateY = (decimal)closestY });
    }

    public static double CalculateDistance(GeoPointDto point1, GeoPointDto point2)
    {
        double deltaX = decimal.ToDouble(point2.CoordinateX - point1.CoordinateX);
        double deltaY = decimal.ToDouble(point2.CoordinateY - point1.CoordinateY);
        return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
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
