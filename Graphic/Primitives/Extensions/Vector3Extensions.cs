using System;
using System.Numerics;

namespace maze.Graphic.Primitives.Extensions
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

        internal static Vector3 LinePlaneIntersection(Vector3 zero, Vector3 point1, Vector3 forward, Vector3 center)
        {
            throw new NotImplementedException();
        }
    }
}