using System.Numerics;
using maze.Engine;

namespace maze.Graphic.Extensions
{
    public static class ScreenExtensions
    {
        public static void RotateX(this Screen screen, float angle)
        {
            screen.CameraForward = screen.CameraForward.RotateX(Vector3.Zero, angle);
            screen.CameraUp = screen.CameraUp.RotateX(Vector3.Zero, angle);
            screen.CameraRight = screen.CameraRight.RotateX(Vector3.Zero, angle);
        }

        public static void RotateY(this Screen screen, float angle)
        {
            screen.CameraForward = screen.CameraForward.RotateY(Vector3.Zero, angle);
            screen.CameraUp = screen.CameraUp.RotateY(Vector3.Zero, angle);
            screen.CameraRight = screen.CameraRight.RotateY(Vector3.Zero, angle);
        }

        public static void MoveForward(this Screen screen, float distance)
        {
            screen.CameraPosition += screen.CameraForward * distance;
        }

        public static void MoveSide(this Screen screen, float distance)
        {
            screen.CameraPosition += screen.CameraRight * distance;
        }
    }
}