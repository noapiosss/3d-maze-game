using System;
using System.Numerics;
using maze.Graphic.Primitives.Extensions;

namespace maze.Engine.Extensions
{
    public static class ScreenExtensions
    {
        public static Vector3 LookAt(this Screen screen, Vector3 vector)
        {
            Vector3 xAxis = screen.CameraRight;
            Vector3 yAxis = screen.CameraUp;
            Vector3 zAxis = screen.CameraForward;
            Vector3 eye = screen.CameraPosition;

            Matrix4x4 lookAtMatrix = new(
                xAxis.X, xAxis.Y, xAxis.Z, Vector3.Dot(xAxis, eye),
                yAxis.X, yAxis.Y, yAxis.Z, Vector3.Dot(yAxis, eye),
                zAxis.X, zAxis.Y, zAxis.Z, Vector3.Dot(zAxis, eye),
                0, 0, 0, 1
            );

            return Vector3.Transform(vector, lookAtMatrix);
        }

        public static Vector3 View(this Screen screen, Vector3 vector)
        {
            Vector3 xAxis = screen.CameraRight;
            Vector3 yAxis = screen.CameraUp;
            Vector3 zAxis = screen.CameraForward;
            Vector3 eye = screen.CameraPosition;

            Matrix4x4 viewMatrix = new(
                xAxis.X, yAxis.X, zAxis.X, 0,
                xAxis.Y, yAxis.Y, zAxis.Y, 0,
                xAxis.Z, yAxis.Z, zAxis.Z, 0,
                -Vector3.Dot(xAxis, eye), -Vector3.Dot(yAxis, eye), -Vector3.Dot(zAxis, eye), 1
            );

            return Vector3.Transform(vector, viewMatrix);
        }
        public static void LookUp(this Screen screen, float angle)
        {
            float nextAngle = screen.CameraForward.Angle(Vector3.UnitY) + angle;
            if (nextAngle is < 0 or > (float)Math.PI)
            {
                return;
            }

            screen.CameraForward = screen.CameraForward.RotateAround(screen.CameraRight, angle);
            screen.CameraUp = screen.CameraUp.RotateAround(screen.CameraRight, angle);
        }

        public static void LookSide(this Screen screen, float angle)
        {
            screen.CameraForward = screen.CameraForward.RotateY(angle);
            screen.CameraUp = screen.CameraUp.RotateY(angle);
            screen.CameraRight = screen.CameraRight.RotateY(angle);
        }

        public static void MoveForward(this Screen screen, float distance)
        {
            screen.CameraPosition += screen.CameraForward * distance;
        }

        public static void MoveUp(this Screen screen, float distance)
        {
            screen.CameraPosition += Vector3.UnitY * distance;
        }

        public static void MoveSide(this Screen screen, float distance)
        {
            screen.CameraPosition += screen.CameraRight * distance;
        }
    }
}