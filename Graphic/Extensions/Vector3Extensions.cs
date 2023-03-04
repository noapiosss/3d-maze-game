using System;
using System.Numerics;
using maze.Engine;

namespace maze.Graphic.Extensions
{
    public static class Vector3Extensions
    {
        public static float Angle(this Vector3 vector1, Vector3 vector2)
        {
            // return (float)Math.Acos(Vector3.Dot(vector1, vector2) / (vector1.Length() * vector2.Length()));
            return (float)Math.Acos((Math.Pow(vector1.Length(), 2) + Math.Pow(vector2.Length(), 2) - Math.Pow((vector1 - vector2).Length(), 2)) / (2 * vector1.Length() * vector2.Length()));
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

        public static Vector3 Projection(this Vector3 vector, Screen screen)
        {
            Vector3 a = vector - screen.CameraPosition;

            Vector3 direction = screen.CameraForward;

            float angleY = new Vector3(direction.X, 0, direction.Z).Angle(Vector3.UnitZ);
            angleY = screen.CameraForward.X >= 0 ? -angleY : angleY;
            a = a.RotateY(angleY);

            direction = direction.RotateY(angleY);

            float angleX = new Vector3(0, direction.Y, direction.Z).Angle(Vector3.UnitZ);
            angleX = screen.CameraForward.Y <= 0 ? -angleX : angleX;
            a = a.RotateX(angleX);

            return a;
        }
    }
}