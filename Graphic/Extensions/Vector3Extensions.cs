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
    }
}