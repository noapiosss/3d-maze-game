using maze.Engine;

namespace maze.Graphic.Extensions
{
    public static class ScreenExtensions
    {
        public static void RotateX(this Screen screen, float angle)
        {
            screen.CameraForward = screen.CameraForward.RotateX(angle);
            screen.CameraUp = screen.CameraUp.RotateX(angle);
            screen.CameraRight = screen.CameraRight.RotateX(angle);
        }

        public static void RotateY(this Screen screen, float angle)
        {
            screen.CameraForward = screen.CameraForward.RotateY(angle);
            screen.CameraUp = screen.CameraUp.RotateY(angle);
            screen.CameraRight = screen.CameraRight.RotateY(angle);
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