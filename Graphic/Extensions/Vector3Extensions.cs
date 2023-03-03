using System;
using System.Numerics;
using Maze.Engine;

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
            Matrix4x4 matrix = new(
                1, 0, 0, 0,
                0, (float)Math.Cos(angle), (float)-Math.Sin(angle), 0,
                0, (float)Math.Sin(angle), (float)Math.Cos(angle), 0,
                0, 0, 0, 1);

            return Vector3.Transform(vector - pivot, matrix) + pivot;
        }

        public static Vector3 RotateY(this Vector3 vector, Vector3 pivot, float angle)
        {
            Matrix4x4 matrix = new(
                (float)Math.Cos(angle), 0, (float)Math.Sin(angle), 0,
                0, 1, 0, 0,
                (float)-Math.Sin(angle), 0, (float)Math.Cos(angle), 0,
                0, 0, 0, 1);

            return Vector3.Transform(vector - pivot, matrix) + pivot;
        }

        public static Vector3 RotateZ(this Vector3 vector, Vector3 pivot, float angle)
        {
            Matrix4x4 matrix = new(
                (float)Math.Cos(angle), (float)-Math.Sin(angle), 0, 0,
                (float)Math.Sin(angle), (float)Math.Cos(angle), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);

            return Vector3.Transform(vector - pivot, matrix) + pivot;
        }

        public static Vector4 Projection(this Vector3 pointPosition, Screen screen)
        {
            Matrix4x4 projectionMatrix = new(
                2 * screen.Near / (screen.Right - screen.Left), 0, 0, -screen.Near * (screen.Right + screen.Left) / (screen.Right - screen.Left),
                0, 2 * screen.Near / (screen.Top - screen.Bottom), 0, -screen.Near * (screen.Top + screen.Bottom) / (screen.Top - screen.Bottom),
                0, 0, -(screen.Far + screen.Near) / (screen.Far - screen.Near), 2 * screen.Far * screen.Near / (screen.Near - screen.Far),
                0, 0, -1, 0
            );

            Matrix4x4 invertedCameraTransforamtion = new(
                screen.CameraRight.X, screen.CameraRight.Y, screen.CameraRight.Z, -((screen.CameraRight.X * screen.CameraPosition.X) + (screen.CameraUp.X * screen.CameraPosition.Y) + (screen.CameraForward.X * screen.CameraPosition.Z)),
                screen.CameraUp.X, screen.CameraUp.Y, screen.CameraUp.Z, -((screen.CameraRight.Y * screen.CameraPosition.X) + (screen.CameraUp.Y * screen.CameraPosition.Y) + (screen.CameraForward.Y * screen.CameraPosition.Z)),
                screen.CameraForward.X, screen.CameraForward.Y, screen.CameraForward.Z, -((screen.CameraRight.Z * screen.CameraPosition.X) + (screen.CameraUp.Z * screen.CameraPosition.Y) + (screen.CameraForward.Z * screen.CameraPosition.Z)),
                0, 0, 0, 1
            );

            // Matrix4x4 pointTransformation = new(
            //     1, 0, 0, pointPosition.X,
            //     0, 1, 0, pointPosition.Y,
            //     0, 0, 1, pointPosition.Z,
            //     0, 0, 0, 1
            // );

            Vector4 position = new(pointPosition, 1);

            return Vector4.Transform(position, projectionMatrix * invertedCameraTransforamtion);
        }
    }
}