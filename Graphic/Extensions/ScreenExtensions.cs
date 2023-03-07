using System;
using System.Numerics;
using maze.Engine;

namespace maze.Graphic.Extensions
{
    public static class ScreenExtensions
    {
        public static void LookUp(this Screen screen, float angle)
        {
            float nextAngle = screen.CameraForward.Angle(Vector3.UnitY) + angle;
            if (nextAngle is < 0 or > (float)Math.PI)
            {
                return;
            }

            screen.CameraForward = screen.CameraForward.RotateAround(screen.CameraRight, angle);
        }

        public static void LookSide(this Screen screen, float angle)
        {
            screen.CameraForward = screen.CameraForward.RotateY(angle);
            screen.CameraRight = screen.CameraRight.RotateY(angle);
        }

        public static void MoveForward(this Screen screen, float distance)
        {
            screen.CameraPosition += screen.CameraForward * distance;
        }

        public static void MoveUp(this Screen screen, float distance)
        {
            screen.CameraPosition += screen.CameraUp * distance;
        }

        public static void MoveSide(this Screen screen, float distance)
        {
            screen.CameraPosition += screen.CameraRight * distance;
        }
    }
}