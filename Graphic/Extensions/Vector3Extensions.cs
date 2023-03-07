using System;
using System.Numerics;
using maze.Engine;

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

        public static Vector3 RotationInOZ(this Vector3 vector, Screen screen)
        {
            Vector3 diretion1 = screen.CameraForward;
            float theta = Vector3.UnitZ.Angle(new(diretion1.X, 0, diretion1.Z));
            theta = diretion1.X < 0 ? -theta : theta;
            Matrix4x4 rotationMatrix1 = new(
                (float)Math.Cos(theta), 0, (float)Math.Sin(theta), 0,
                0, 1, 0, 0,
                (float)-Math.Sin(theta), 0, (float)Math.Cos(theta), 0,
                0, 0, 0, 0
            );

            Vector3 direction2 = Vector3.Transform(diretion1, rotationMatrix1);
            float phi = Vector3.UnitZ.Angle(direction2);
            phi = direction2.Y > 0 ? -phi : phi;
            Matrix4x4 rotationMatrix2 = new(
                1, 0, 0, 0,
                0, (float)Math.Cos(phi), (float)-Math.Sin(phi), 0,
                0, (float)Math.Sin(phi), (float)Math.Cos(phi), 0,
                0, 0, 0, 0
            );

            return Vector3.Transform(vector - screen.CameraPosition, rotationMatrix1 * rotationMatrix2);
        }

        public static Vector3 NormalRotationInOZ(this Vector3 normal, Screen screen)
        {
            Vector3 diretion1 = screen.CameraForward;
            float theta = Vector3.UnitZ.Angle(new(diretion1.X, 0, diretion1.Z));
            theta = diretion1.X < 0 ? -theta : theta;
            Matrix4x4 rotationMatrix1 = new(
                (float)Math.Cos(theta), 0, (float)Math.Sin(theta), 0,
                0, 1, 0, 0,
                (float)-Math.Sin(theta), 0, (float)Math.Cos(theta), 0,
                0, 0, 0, 0
            );

            Vector3 direction2 = Vector3.Transform(diretion1, rotationMatrix1);
            float phi = Vector3.UnitZ.Angle(direction2);
            phi = direction2.Y > 0 ? -phi : phi;
            Matrix4x4 rotationMatrix2 = new(
                1, 0, 0, 0,
                0, (float)Math.Cos(phi), (float)-Math.Sin(phi), 0,
                0, (float)Math.Sin(phi), (float)Math.Cos(phi), 0,
                0, 0, 0, 0
            );

            return Vector3.Transform(normal, rotationMatrix1 * rotationMatrix2);
        }



        public static Vector3 RotateToOZDirection(this Vector3 vector)
        {
            Vector3 diretion1 = vector;
            float theta = Vector3.UnitZ.Angle(new(diretion1.X, 0, diretion1.Z));
            theta = diretion1.X < 0 ? -theta : theta;
            Matrix4x4 rotationMatrix1 = new(
                (float)Math.Cos(theta), 0, (float)Math.Sin(theta), 0,
                0, 1, 0, 0,
                (float)-Math.Sin(theta), 0, (float)Math.Cos(theta), 0,
                0, 0, 0, 0
            );

            Vector3 direction2 = Vector3.Transform(diretion1, rotationMatrix1);
            float phi = Vector3.UnitZ.Angle(direction2);
            phi = direction2.Y > 0 ? -phi : phi;
            Matrix4x4 rotationMatrix2 = new(
                1, 0, 0, 0,
                0, (float)Math.Cos(phi), (float)-Math.Sin(phi), 0,
                0, (float)Math.Sin(phi), (float)Math.Cos(phi), 0,
                0, 0, 0, 0
            );

            return Vector3.Transform(vector, rotationMatrix1 * rotationMatrix2);
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
    }
}