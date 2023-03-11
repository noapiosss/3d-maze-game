using System;
using System.Numerics;

namespace maze.Graphic.Primitives.Helpres
{
    public static class Intersections
    {
        public static bool LineLineIntersection(Vector3 line1Start, Vector3 line1End, Vector3 line2Start, Vector3 line2End, out Vector3 intersection)
        {
            intersection = Vector3.Zero;

            Vector3 line1Direction = line1End - line1Start;
            Vector3 line2Direction = line2End - line2Start;

            Vector3 cross1 = Vector3.Cross(line1Direction, line2Direction);
            Vector3 cross2 = Vector3.Cross(line2Start - line1Start, line1Direction);

            float denominator = cross1.Length();
            if (denominator == 0)
            {
                return false;
            }

            float numerator = Vector3.Dot(cross2, cross1) / denominator;

            intersection = line2Start + (line2Direction * numerator);
            return true;
        }

        public static bool LinePlaneIntersection(Vector3 lineStart, Vector3 lineEnd, Vector3 planeNormal, Vector3 planePoint, out Vector3 intersection)
        {
            intersection = Vector3.Zero;

            Vector3 lineDriection = lineEnd - lineStart;

            float denominator = Vector3.Dot(planeNormal, lineDriection);
            if (denominator == 0)
            {
                return false;
            }

            float numerator = Vector3.Dot(planeNormal, planePoint - lineStart);
            float distance = numerator / denominator;

            intersection = lineStart + (lineDriection * distance);
            return true;
        }

        public static bool LinePlaneIntersection(Vector3 lineStart, Vector3 lineEnd, Vector3 planePoint1, Vector3 planePoint2, Vector3 planePoint3, out Vector3 intersection)
        {
            intersection = Vector3.Zero;

            Vector3 lineDriection = lineEnd - lineStart;
            Vector3 planeNormal = Vector3.Cross(planePoint2 - planePoint1, planePoint3 - planePoint1);

            float denominator = Vector3.Dot(planeNormal, lineDriection);
            if (denominator == 0)
            {
                return false;
            }

            float numerator = Vector3.Dot(planeNormal, planePoint1 - lineStart);
            float distance = numerator / denominator;

            intersection = lineStart + (lineDriection * distance);
            return true;
        }

        public static bool LineSphereIntersection(Vector3 lineStart, Vector3 lineEnd, Vector3 center, float radius, out Vector3 intersection1, out Vector3 intersection2)
        {
            intersection1 = Vector3.Zero;
            intersection2 = Vector3.Zero;

            Vector3 lineDirection = lineEnd - lineStart;
            Vector3 lineToCenter = lineStart - center;

            float a = Vector3.Dot(lineDirection, lineDirection);
            float b = 2 * Vector3.Dot(lineToCenter, lineDirection);
            float c = Vector3.Dot(lineToCenter, lineToCenter) - (radius * radius);

            float discriminant = (b * b) - (4 * a * c);
            if (discriminant < 0)
            {
                return false;
            }

            float t1 = (-b + (float)Math.Sqrt(discriminant)) / (2 * a);
            float t2 = (-b - (float)Math.Sqrt(discriminant)) / (2 * a);

            intersection1 = lineStart + (t1 * lineDirection);
            intersection2 = lineStart + (t2 * lineDirection);

            return true;
        }

        public static Vector3 GetAnyLineNormal(Vector3 lineStart, Vector3 lineEnd)
        {
            Vector3 lineDirection = Vector3.Normalize(lineEnd - lineStart);
            Vector3 arbitraryVector = Vector3.UnitX;

            Vector3 normal = Vector3.Cross(lineDirection, arbitraryVector);

            if (normal == Vector3.Zero)
            {
                arbitraryVector = Vector3.UnitY;
                normal = Vector3.Cross(lineDirection, arbitraryVector);
            }

            return Vector3.Normalize(normal);
        }

    }
}