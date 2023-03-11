using System.Numerics;

namespace maze.Engine.Extensions
{
    public static class CameraExtensions
    {
        public static Vector3 LookAt(this Camera camera, Vector3 vector)
        {
            Vector3 xAxis = camera.Right;
            Vector3 yAxis = camera.Up;
            Vector3 zAxis = camera.Forward;
            Vector3 eye = camera.Position;

            Matrix4x4 lookAtMatrix = new(
                xAxis.X, xAxis.Y, xAxis.Z, Vector3.Dot(xAxis, eye),
                yAxis.X, yAxis.Y, yAxis.Z, Vector3.Dot(yAxis, eye),
                zAxis.X, zAxis.Y, zAxis.Z, Vector3.Dot(zAxis, eye),
                0, 0, 0, 1
            );

            return Vector3.Transform(vector, lookAtMatrix);
        }

        public static Vector3 View(this Camera camera, Vector3 vector)
        {
            Vector3 xAxis = camera.Right;
            Vector3 yAxis = camera.Up;
            Vector3 zAxis = camera.Forward;
            Vector3 eye = camera.Position;

            Matrix4x4 viewMatrix = new(
                xAxis.X, yAxis.X, zAxis.X, 0,
                xAxis.Y, yAxis.Y, zAxis.Y, 0,
                xAxis.Z, yAxis.Z, zAxis.Z, 0,
                -Vector3.Dot(xAxis, eye), -Vector3.Dot(yAxis, eye), -Vector3.Dot(zAxis, eye), 1
            );

            return Vector3.Transform(vector, viewMatrix);
        }
    }
}