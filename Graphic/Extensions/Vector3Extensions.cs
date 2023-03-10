using System;
using System.Numerics;

namespace maze.Graphic.Extensions
{
    public static class Vector3Extensions
    {
        public static float Angle(this Vector3 vector1, Vector3 vector2)
        {
            return (float)Math.Acos(Vector3.Dot(vector1, vector2) / (vector1.Length() * vector2.Length()));
        }

        public static Vector3 RotateX(this Vector3 vector, Vector3 pivot, float angle)
        {
            return Vector3.Transform(vector, Matrix4x4.CreateRotationX(angle, pivot));
        }

        public static Vector3 RotateX(this Vector3 vector, float angle)
        {
            return Vector3.Transform(vector, Matrix4x4.CreateRotationX(angle));
        }

        public static Vector3 RotateY(this Vector3 vector, Vector3 pivot, float angle)
        {
            return Vector3.Transform(vector, Matrix4x4.CreateRotationY(angle, pivot));
        }
        public static Vector3 RotateY(this Vector3 vector, float angle)
        {
            return Vector3.Transform(vector, Matrix4x4.CreateRotationY(angle));
        }

        public static Vector3 RotateZ(this Vector3 vector, Vector3 pivot, float angle)
        {
            return Vector3.Transform(vector, Matrix4x4.CreateRotationZ(angle, pivot));
        }

        public static Vector3 RotateZ(this Vector3 vector, float angle)
        {
            return Vector3.Transform(vector, Matrix4x4.CreateRotationZ(angle));
        }

        public static Vector3 RotateAround(this Vector3 vector, Vector3 axis, float angle)
        {
            Matrix4x4 rotation = new(
                (float)(Math.Cos(angle) + (axis.X * axis.X * (1 - Math.Cos(angle)))), (float)((axis.X * axis.Y * (1 - Math.Cos(angle))) - (axis.Z * Math.Sin(angle))), (float)((axis.X * axis.Z * (1 - Math.Cos(angle))) + (axis.Y * Math.Cos(angle))), 0,
                (float)((axis.Y * axis.X * (1 - Math.Cos(angle))) + (axis.Z * Math.Sin(angle))), (float)(Math.Cos(angle) + (axis.Y * axis.Y * (1 - Math.Cos(angle)))), (float)((axis.Y * axis.Z * (1 - Math.Cos(angle))) - (axis.X * Math.Sin(angle))), 0,
                (float)((axis.Z * axis.X * (1 - Math.Cos(angle))) + (axis.Y * Math.Sin(angle))), (float)((axis.Z * axis.Y * (1 - Math.Cos(angle))) + (axis.X * Math.Sin(angle))), (float)(Math.Cos(angle) + (axis.Z * axis.Z * (1 - Math.Cos(angle)))), 0,
                0, 0, 0, 0
            );

            return Vector3.Transform(vector, rotation);
        }

        public static Vector3 LineLineIntersection(Vector3 a1, Vector3 b1, Vector3 a2, Vector3 b2)
        {
            Vector3 line1Direction = b1 - a1;
            Vector3 line2Direction = b2 - a2;

            Vector3 cross1 = Vector3.Cross(line1Direction, line2Direction);
            Vector3 cross2 = Vector3.Cross(a2 - a1, line1Direction);

            float denominator = cross1.Length();
            float numerator = Vector3.Dot(cross2, cross1) / denominator;

            return a2 + (line2Direction * numerator);
        }

        public static Vector3 LinePlaneIntersection(Vector3 a, Vector3 b, Vector3 planeNormal, Vector3 planePoint)
        {
            Vector3 lineDriection = b - a;

            float denominator = Vector3.Dot(planeNormal, lineDriection);
            float numerator = Vector3.Dot(planeNormal, planePoint - a);

            float distance = numerator / denominator;

            return a + (lineDriection * distance);
        }

        public static Vector3 LinePlaneIntersection(Vector3 a, Vector3 b, Vector3 planePoint1, Vector3 planePoint2, Vector3 planePoint3)
        {
            Vector3 lineDriection = b - a;
            Vector3 planeNormal = Vector3.Cross(planePoint2 - planePoint1, planePoint3 - planePoint1);

            float denominator = Vector3.Dot(planeNormal, lineDriection);
            float numerator = Vector3.Dot(planeNormal, planePoint1 - a);

            float distance = numerator / denominator;

            return a + (lineDriection * distance);
        }

        public static Vector3 GetAnyLineNormal(Vector3 a, Vector3 b)
        {
            Vector3 lineDirection = Vector3.Normalize(b - a);
            Vector3 arbitraryVector = Vector3.UnitX;

            Vector3 normal = Vector3.Cross(lineDirection, arbitraryVector);

            if (normal == Vector3.Zero)
            {
                arbitraryVector = Vector3.UnitY;
                normal = Vector3.Cross(lineDirection, arbitraryVector);
            }

            return Vector3.Normalize(normal);
        }

        public static Vector3 LineSphereIntersection(Vector3 lineStart, Vector3 lineEnd, Vector3 center, float radius)
        {
            Vector3 lineDirection = lineEnd - lineStart;
            Vector3 lineToCenter = lineStart - center;

            float a = Vector3.Dot(lineDirection, lineDirection);
            float b = 2 * Vector3.Dot(lineToCenter, lineDirection);
            float c = Vector3.Dot(lineToCenter, lineToCenter) - (radius * radius);

            float discriminant = (b * b) - (4 * a * c);

            float t1 = (-b + (float)Math.Sqrt(discriminant)) / (2 * a);
            float t2 = (-b - (float)Math.Sqrt(discriminant)) / (2 * a);

            Vector3 intersection1 = lineStart + (t1 * lineDirection);
            Vector3 intersection2 = lineStart + (t2 * lineDirection);

            return Vector3.Distance(intersection1, lineStart) < Vector3.Distance(intersection2, lineStart) ? intersection1 : intersection2;
        }

        public static Ellipse GetEllipse(Vector3 W, Vector3 X, Vector3 Y, Vector3 Z)
        {
            float m00 = (X.X * Y.X * Z.Y) - (W.X * Y.X * Z.Y) - (X.X * Y.Y * Z.X) + (W.X * Y.Y * Z.X) - (W.X * X.Y * Z.X) + (W.Y * X.X * Z.X) + (W.X * X.Y * Y.X) - (W.Y * X.X * Y.X);
            float m01 = (W.X * Y.X * Z.Y) - (W.X * X.X * Z.Y) - (X.X * Y.Y * Z.X) + (X.Y * Y.X * Z.X) - (W.Y * Y.X * Z.X) + (W.Y * X.X * Z.X) + (W.X * X.X * Y.Y) - (W.X * X.Y * Y.X);
            float m02 = (X.X * Y.X * Z.Y) - (W.X * X.X * Z.Y) - (W.X * Y.Y * Z.X) - (X.Y * Y.X * Z.X) + (W.Y * Y.X * Z.X) + (W.X * X.Y * Z.X) + (W.X * X.X * Y.Y) - (W.Y * X.X * Y.X);
            float m10 = (X.Y * Y.X * Z.Y) - (W.Y * Y.X * Z.Y) - (W.X * X.Y * Z.Y) + (W.Y * X.X * Z.Y) - (X.Y * Y.Y * Z.X) + (W.Y * Y.Y * Z.X) + (W.X * X.Y * Y.Y) - (W.Y * X.X * Y.Y);
            float m11 = (-X.X * Y.Y * Z.Y) + (W.X * Y.Y * Z.Y) + (X.Y * Y.X * Z.Y) - (W.X * X.Y * Z.Y) - (W.Y * Y.Y * Z.X) + (W.Y * X.Y * Z.X) + (W.Y * X.X * Y.Y) - (W.Y * X.Y * Y.X);
            float m12 = (X.X * Y.Y * Z.Y) - (W.X * Y.Y * Z.Y) + (W.Y * Y.X * Z.Y) - (W.Y * X.X * Z.Y) - (X.Y * Y.Y * Z.X) + (W.Y * X.Y * Z.X) + (W.X * X.Y * Y.Y) - (W.Y * X.Y * Y.X);
            float m20 = (X.X * Z.Y) - (W.X * Z.Y) - (X.Y * Z.X) + (W.Y * Z.X) - (X.X * Y.Y) + (W.X * Y.Y) + (X.Y * Y.X) - (W.Y * Y.X);
            float m21 = (Y.X * Z.Y) - (X.X * Z.Y) - (Y.Y * Z.X) + (X.Y * Z.X) + (W.X * Y.Y) - (W.Y * Y.X) - (W.X * X.Y) + (W.Y * X.X);
            float m22 = (Y.X * Z.Y) - (W.X * Z.Y) - (Y.Y * Z.X) + (W.Y * Z.X) + (X.X * Y.Y) - (X.Y * Y.X) + (W.X * X.Y) - (W.Y * X.X);

            float determinant = (m00 * ((m11 * m22) - (m21 * m12))) - (m01 * ((m10 * m22) - (m12 * m20))) + (m02 * ((m10 * m21) - (m11 * m20)));

            float J = ((m11 * m22) - (m21 * m12)) / determinant;
            float K = -((m01 * m22) - (m02 * m21)) / determinant;
            float L = ((m01 * m12) - (m02 * m11)) / determinant;
            float M = -((m10 * m22) - (m12 * m20)) / determinant;
            float N = ((m00 * m22) - (m02 * m20)) / determinant;
            float O = -((m00 * m12) - (m10 * m02)) / determinant;
            float P = ((m10 * m21) - (m20 * m11)) / determinant;
            float Q = -((m00 * m21) - (m20 * m01)) / determinant;
            float R = ((m00 * m11) - (m10 * m01)) / determinant;

            float a = (J * J) + (M * M) - (P * P);
            float b = (J * K) + (M * N) - (P * Q);
            float c = (K * K) + (N * N) - (Q * Q);
            float d = (J * L) + (M * O) - (P * R);
            float f = (K * L) + (N * O) - (Q * R);
            float g = (L * L) + (O * O) - (R * R);

            float centerX = ((c * d) - (b * f)) / ((b * b) - (a * c));
            float centerY = ((a * f) - (b * d)) / ((b * b) - (a * c));

            float radiusA = (float)Math.Sqrt(2 * ((a * f * f) + (c * d * d) + (g * b * b) - (2 * b * d * f) - (a * c * g)) / (((b * b) - (a * c)) * (Math.Sqrt(((a - c) * (a - c)) + (4 * b * b)) - (a + c))));
            float radiusB = (float)Math.Sqrt(2 * ((a * f * f) + (c * d * d) + (g * b * b) - (2 * b * d * f) - (a * c * g)) / (((b * b) - (a * c)) * (-Math.Sqrt(((a - c) * (a - c)) + (4 * b * b)) - (a + c))));

            float angle = 0;

            if (b == 0 && a <= c)
            {
                angle = 0;
            }
            else if (b == 0 && a >= c)
            {
                angle = (float)(Math.PI / 2);
            }
            else if (b != 0 && a > c)
            {
                angle = (float)((Math.PI / 2) + (0.5 * ((Math.PI / 2) - Math.Atan2(a - c, 2 * b))));
            }
            else if (b != 0 && a <= c)
            {
                angle = (float)((Math.PI / 2) + (0.5 * ((Math.PI / 2) - Math.Atan2(a - c, 2 * b))));
            }

            return new()
            {
                CenterX = centerX,
                CenterY = centerY,
                RadiusA = radiusA,
                RadiusB = radiusB,
                Angle = angle
            };
        }
    }
}